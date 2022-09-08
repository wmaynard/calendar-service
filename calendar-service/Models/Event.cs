using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using Rumble.Platform.Common.Models;
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable ArrangeAttributes

namespace Rumble.Platform.CalendarService.Models;

[BsonIgnoreExtraElements]
public class Event : PlatformCollectionDocument
{
    internal const string DB_KEY_TITLE       = "title";
    internal const string DB_KEY_DESCRIPTION = "desc";
    internal const string DB_KEY_START       = "start";
    internal const string DB_KEY_END         = "end";
    internal const string DB_KEY_REPEAT      = "rpt";

    public const string FRIENDLY_KEY_TITLE       = "title";
    public const string FRIENDLY_KEY_DESCRIPTION = "description";
    public const string FRIENDLY_KEY_START       = "start";
    public const string FRIENDLY_KEY_END         = "end";
    public const string FRIENDLY_KEY_REPEAT      = "repeat";
    
    [BsonElement(DB_KEY_TITLE)]
    [JsonInclude, JsonPropertyName(FRIENDLY_KEY_TITLE)]
    public string Title { get; set; }
    
    [BsonElement(DB_KEY_DESCRIPTION)]
    [JsonInclude, JsonPropertyName(FRIENDLY_KEY_DESCRIPTION)]
    public string Description { get; set; }
    
    [BsonElement(DB_KEY_START)]
    [JsonInclude, JsonPropertyName(FRIENDLY_KEY_START)]
    public long Start { get; set; }
    
    [BsonElement(DB_KEY_END)]
    [JsonInclude, JsonPropertyName(FRIENDLY_KEY_END)]
    public long End { get; set; }
    
    [BsonElement(DB_KEY_REPEAT), BsonIgnoreIfNull]
    [JsonInclude, JsonPropertyName(FRIENDLY_KEY_REPEAT), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DayOfWeek Repeat { get; set; } // 0-6, 0 starting with sunday

    protected override void Validate(out List<string> errors)
    {
        errors = new List<string>();
        if (Title == null)
        {
            errors.Add("Title cannot be null.");
        }
        
        if (Description == null)
        {
            errors.Add("Description cannot be null.");
        }

        if (Start is < 1_000_000_000 or >= 10_000_000_000) // in case not s unix time (not 10 digits)
        {
            errors.Add("Start time is invalid.");
        }
        
        if (End is < 1_000_000_000 or >= 10_000_000_000) // in case not s unix time (not 10 digits)
        {
            errors.Add("End time is invalid.");
        }
            
    }
}