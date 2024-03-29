﻿using System;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.IO.Abstractions;

namespace LaTeXTools.Project;

/// <summary>
/// Models a LaTeX project
/// </summary>
public sealed class LaTeXProject
{
    /// <summary>
    /// The latex engine that we use.
    /// </summary>
    [JsonPropertyName("latex")]
    public string LaTeX { get; set; } = "pdflatex";

    /// <summary>
    /// Where to place outputs
    /// </summary>
    [JsonPropertyName("bin")]
    public string Bin { get; set; } = "bin";

    /// <summary>
    /// Entry <c>.tex</c> file of the project
    /// </summary>
    [JsonPropertyName("entry")]
    public string Entry { get; set; } = "index.tex";

    /// <summary>
    /// Bibliography file of the project
    /// </summary>
    [JsonPropertyName("bib")]
    public string Bib { get; set; } = "none";

    /// <summary>
    /// If to enable glossary for this project
    /// </summary>
    [JsonPropertyName("glossary")]
    public bool Glossary { get; set; } = false;

    /// <summary>
    /// Files/directories included by <c>Entry</c>
    /// </summary>
    [JsonPropertyName("includes")]
    public string[] Includes { get; set; } = new string[] { };

    /// <summary>
    /// Working directory of the project, i.e. the folder in which the project file is in
    /// </summary>
    /// <value></value>
    [JsonIgnore]
    public string WorkingDirectory { get; set; } = Environment.CurrentDirectory;

    /// <summary>
    /// Write the project to a file at path
    /// </summary>
    /// <param name="path">the path of the file to be written to</param>
    /// <returns></returns>
    public async ValueTask WriteAsync(string path, IFile file)
    {
        using Stream fileStream = file.OpenWrite(path);

        var options = new JsonSerializerOptions()
        {
            WriteIndented = true,
        };

        var context = new LaTeXJsonContext(options);
        
        await JsonSerializer.SerializeAsync(
            fileStream, this, typeof(LaTeXProject), context);
    }

    /// <summary>
    /// Search up from the current working directory to find a project file with a specified
    /// name
    /// </summary>
    /// <param name="filename">the name of the project file to find</param>
    /// <returns><c>null</c> if not found, otherwise return the project</returns>
    public static async ValueTask<LaTeXProject?> FindAsync(string filename)
    {
        string current = Environment.CurrentDirectory;
        string root = Path.GetPathRoot(current)!;

        while (!File.Exists(Path.Combine(current, filename)) || current == root)
        {
            string? parent = Path.GetDirectoryName(current);

            if (parent == null)
            {
                return null;
            }

            current = parent;
        }

        string path = Path.Combine(current, filename);

        if (File.Exists(path))
        {
            return await LoadAsync(path);
        }

        return null;
    }

    /// <summary>
    /// Load a project from a project file.
    /// </summary>
    /// <param name="path">the path of the file to load from</param>
    /// <returns>The loaded project</returns>
    public static async ValueTask<LaTeXProject> LoadAsync(string path)
    {
        if (!Path.IsPathRooted(path))
        {
            throw new ArgumentException($"{path} is not rooted!");
        }

        using var stream = File.Open(path, FileMode.Open);
        string directory = Path.GetDirectoryName(path)!;

        var project = (LaTeXProject?)await JsonSerializer.DeserializeAsync(
            stream, typeof(LaTeXProject), LaTeXJsonContext.Default);

        if (project == null)
        {
            throw new Exception($"Failed to parse project at {path}");
        }

        project.WorkingDirectory = directory;

        return project;
    }
}
