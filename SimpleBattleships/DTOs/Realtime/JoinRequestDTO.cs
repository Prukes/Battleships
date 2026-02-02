using System.ComponentModel.DataAnnotations;

namespace Battleships.DTOs.Realtime;

public class JoinRequestDto
{
    public required string PlayerOneName { get; set; }
    public required string PlayerTwoName { get; set; }
    [Range(10,20)]
    public required int BoardSize { get; set; }
}