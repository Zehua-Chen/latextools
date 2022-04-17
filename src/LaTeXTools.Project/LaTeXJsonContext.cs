using System.Text.Json;
using System.Text.Json.Serialization;

namespace LaTeXTools.Project;

/// <summary>
/// Json Context for serialization
/// </summary>
[JsonSerializable(typeof(LaTeXProject))]
internal partial class LaTeXJsonContext : JsonSerializerContext {}
