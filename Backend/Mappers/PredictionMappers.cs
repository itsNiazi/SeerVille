using System.ComponentModel.DataAnnotations;
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
            PredictionDate = prediction.PredictionDate,
            ResolutionDate = prediction.ResolutionDate,
            IsResolved = prediction.IsResolved,
            IsCorrect = prediction.IsCorrect,
            ResolvedAt = prediction.ResolvedAt,
        };
    }

    public static Prediction ToPredictionEntity(this CreatePredictionDto prediction)
    {
        return new Prediction
        {
            PredictionId = Guid.NewGuid(),
            CreatorId = prediction.CreatorId,// should be from session and not provided in the CreatePredictionDto!!
            TopicId = prediction.TopicId,
            PredictionName = prediction.PredictionName,
            PredictionDate = DateTime.UtcNow,
            ResolutionDate = prediction.ResolutionDate,
            IsResolved = false,
            IsCorrect = false,
        };
    }
}
