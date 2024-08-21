using System.Text.Json.Serialization;

namespace DutyCycle.Models.Machine
{
    public class MachineDto
    {
        [JsonInclude] public required uint DeviceType;
        [JsonInclude] public required uint BoardID;
        [JsonInclude] public required int AxesCount;
        [JsonInclude] public required MachineParameters Parameters;
        [JsonInclude] public required TestConditions TestConditions;
        [JsonInclude] public required string advantechConfigurationPath;
    }
}
