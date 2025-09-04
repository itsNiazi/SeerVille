using Backend.DTOs;
using Backend.Entities;

namespace Backend.Mappers;

public static class PredictionMappers
{
    public static PredictionDto ToPredictionDto(this Prediction prediction)
    {
        return new PredictionDto
        {
            PredictionId = prediction.PredictionId,
            CreatorId = prediction.CreatorId,
            TopicId = prediction.TopicId,
            PredictionName = prediction.PredictionName,
            PredictionRules = prediction.PredictionRules,
            PredictionDate = prediction.PredictionDate,
            ResolutionDate = prediction.ResolutionDate,
            IsResolved = prediction.IsResolved,
            IsCorrect = prediction.IsCorrect,
            ResolvedAt = prediction.ResolvedAt,
        };
    }

    public static Prediction ToPredictionEntity(this CreatePredictionDto prediction, string userId)
    {
        return new Prediction
        {
            PredictionId = Guid.NewGuid(),
            CreatorId = Guid.Parse(userId),
            TopicId = prediction.TopicId,
            PredictionName = prediction.PredictionName,
            PredictionRules = "Temp text", //fix this
            PredictionDate = DateTime.UtcNow,
            ResolutionDate = prediction.ResolutionDate,
            IsResolved = false,
            IsCorrect = false,
        };
    }
}
