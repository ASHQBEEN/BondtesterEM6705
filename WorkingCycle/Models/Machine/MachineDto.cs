using System.Text.Json.Serialization;

namespace DutyCycle.Models.Machine
{
    public class MachineDto
    {
        [JsonInclude] public required MachineParameters Parameters;
        [JsonInclude] public required TestConditions TestConditions;
        [JsonInclude] public required CameraParameters CameraParameters;
        [JsonInclude] public required string advantechConfigurationPath;
    }
}
