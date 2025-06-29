using Backend.DTOs;
using Backend.Entities;

namespace Backend.Mappers;

public static class PredictionVoteMappers
{
    public static PredictionVoteDto ToPredictionVoteDto(this PredictionVote vote)
    {
        return new PredictionVoteDto
        {
            VoteId = vote.VoteId,
            PredictionId = vote.PredictionId,
            UserId = vote.UserId,
            PredictedOutcome = vote.PredictedOutcome,
            VotedAt = vote.VotedAt
        };
    }

    public static PredictionVote ToPredictionVoteEntity(this CreatePredictionVoteDto vote)
    {
        return new PredictionVote
        {
            VoteId = Guid.NewGuid(),
            PredictionId = vote.PredictionId,
            UserId = vote.UserId,
            PredictedOutcome = vote.PredictedOutcome,
            VotedAt = DateTime.UtcNow
        };
    }
}
