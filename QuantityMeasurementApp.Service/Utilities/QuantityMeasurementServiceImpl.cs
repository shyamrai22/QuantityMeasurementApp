using QuantityMeasurementApp.Library.Model;
using QuantityMeasurementApp.Model.DTO;
using QuantityMeasurementApp.Model.Entity;
using QuantityMeasurementApp.Repository;

namespace QuantityMeasurementApp.Service
{
  public class QuantityMeasurementServiceImpl : IQuantityMeasurementService
  {
    private readonly IQuantityMeasurementRepository repository;

    public QuantityMeasurementServiceImpl(IQuantityMeasurementRepository repository)
    {
      this.repository = repository;
    }

    public bool Compare(QuantityDTO firstQuantityDto, QuantityDTO secondQuantityDto)
    {
      bool operationResult = firstQuantityDto.MeasurementType.ToLower() switch
      {
        "length" => new Quantity<LengthUnit>(firstQuantityDto.Value, Enum.Parse<LengthUnit>(firstQuantityDto.Unit, true))
                        .Equals(new Quantity<LengthUnit>(secondQuantityDto.Value, Enum.Parse<LengthUnit>(secondQuantityDto.Unit, true))),

        "weight" => new Quantity<WeightUnit>(firstQuantityDto.Value, Enum.Parse<WeightUnit>(firstQuantityDto.Unit, true))
                        .Equals(new Quantity<WeightUnit>(secondQuantityDto.Value, Enum.Parse<WeightUnit>(secondQuantityDto.Unit, true))),

        "volume" => new Quantity<VolumeUnit>(firstQuantityDto.Value, Enum.Parse<VolumeUnit>(firstQuantityDto.Unit, true))
                        .Equals(new Quantity<VolumeUnit>(secondQuantityDto.Value, Enum.Parse<VolumeUnit>(secondQuantityDto.Unit, true))),

        "temperature" => new Quantity<TemperatureUnit>(firstQuantityDto.Value, Enum.Parse<TemperatureUnit>(firstQuantityDto.Unit, true))
                        .Equals(new Quantity<TemperatureUnit>(secondQuantityDto.Value, Enum.Parse<TemperatureUnit>(secondQuantityDto.Unit, true))),

        _ => throw new ArgumentException("Unsupported measurement type")
      };

      repository.Save(new QuantityMeasurementEntity("COMPARE", firstQuantityDto, secondQuantityDto, operationResult));

      return operationResult;
    }

    public QuantityDTO Convert(QuantityDTO sourceQuantityDto, string targetUnit)
    {
      object conversionResult = sourceQuantityDto.MeasurementType.ToLower() switch
      {
        "length" => new Quantity<LengthUnit>(sourceQuantityDto.Value, Enum.Parse<LengthUnit>(sourceQuantityDto.Unit, true))
                        .ConvertTo(Enum.Parse<LengthUnit>(targetUnit, true)),

        "weight" => new Quantity<WeightUnit>(sourceQuantityDto.Value, Enum.Parse<WeightUnit>(sourceQuantityDto.Unit, true))
                        .ConvertTo(Enum.Parse<WeightUnit>(targetUnit, true)),

        "volume" => new Quantity<VolumeUnit>(sourceQuantityDto.Value, Enum.Parse<VolumeUnit>(sourceQuantityDto.Unit, true))
                        .ConvertTo(Enum.Parse<VolumeUnit>(targetUnit, true)),

        "temperature" => new Quantity<TemperatureUnit>(sourceQuantityDto.Value, Enum.Parse<TemperatureUnit>(sourceQuantityDto.Unit, true))
                        .ConvertTo(Enum.Parse<TemperatureUnit>(targetUnit, true)),

        _ => throw new ArgumentException("Unsupported measurement type")
      };

      dynamic resultQuantity = conversionResult;

      var resultDto = new QuantityDTO(resultQuantity.Value, resultQuantity.Unit.ToString(), sourceQuantityDto.MeasurementType);

      repository.Save(new QuantityMeasurementEntity("CONVERT", sourceQuantityDto, null, resultDto));

      return resultDto;
    }

    public QuantityDTO Add(QuantityDTO firstQuantityDto, QuantityDTO secondQuantityDto)
    {
      object additionResult = firstQuantityDto.MeasurementType.ToLower() switch
      {
        "length" => new Quantity<LengthUnit>(firstQuantityDto.Value, Enum.Parse<LengthUnit>(firstQuantityDto.Unit, true))
                        .Add(new Quantity<LengthUnit>(secondQuantityDto.Value, Enum.Parse<LengthUnit>(secondQuantityDto.Unit, true))),

        "weight" => new Quantity<WeightUnit>(firstQuantityDto.Value, Enum.Parse<WeightUnit>(firstQuantityDto.Unit, true))
                        .Add(new Quantity<WeightUnit>(secondQuantityDto.Value, Enum.Parse<WeightUnit>(secondQuantityDto.Unit, true))),

        "volume" => new Quantity<VolumeUnit>(firstQuantityDto.Value, Enum.Parse<VolumeUnit>(firstQuantityDto.Unit, true))
                        .Add(new Quantity<VolumeUnit>(secondQuantityDto.Value, Enum.Parse<VolumeUnit>(secondQuantityDto.Unit, true))),

        "temperature" => throw new InvalidOperationException("Temperature does not support addition"),

        _ => throw new ArgumentException("Unsupported measurement type")
      };

      dynamic resultQuantity = additionResult;

      var resultDto = new QuantityDTO(resultQuantity.Value, resultQuantity.Unit.ToString(), firstQuantityDto.MeasurementType);

      repository.Save(new QuantityMeasurementEntity("ADD", firstQuantityDto, secondQuantityDto, resultDto));

      return resultDto;
    }

    public QuantityDTO Subtract(QuantityDTO firstQuantityDto, QuantityDTO secondQuantityDto)
    {
      object subtractionResult = firstQuantityDto.MeasurementType.ToLower() switch
      {
        "length" => new Quantity<LengthUnit>(firstQuantityDto.Value, Enum.Parse<LengthUnit>(firstQuantityDto.Unit, true))
                        .Subtract(new Quantity<LengthUnit>(secondQuantityDto.Value, Enum.Parse<LengthUnit>(secondQuantityDto.Unit, true))),

        "weight" => new Quantity<WeightUnit>(firstQuantityDto.Value, Enum.Parse<WeightUnit>(firstQuantityDto.Unit, true))
                        .Subtract(new Quantity<WeightUnit>(secondQuantityDto.Value, Enum.Parse<WeightUnit>(secondQuantityDto.Unit, true))),

        "volume" => new Quantity<VolumeUnit>(firstQuantityDto.Value, Enum.Parse<VolumeUnit>(firstQuantityDto.Unit, true))
                        .Subtract(new Quantity<VolumeUnit>(secondQuantityDto.Value, Enum.Parse<VolumeUnit>(secondQuantityDto.Unit, true))),

        "temperature" => throw new InvalidOperationException("Temperature does not support subtraction"),

        _ => throw new ArgumentException("Unsupported measurement type")
      };

      dynamic resultQuantity = subtractionResult;

      var resultDto = new QuantityDTO(resultQuantity.Value, resultQuantity.Unit.ToString(), firstQuantityDto.MeasurementType);

      repository.Save(new QuantityMeasurementEntity("SUBTRACT", firstQuantityDto, secondQuantityDto, resultDto));

      return resultDto;
    }

    public double Divide(QuantityDTO firstQuantityDto, QuantityDTO secondQuantityDto)
    {
      return firstQuantityDto.MeasurementType.ToLower() switch
      {
        "length" => new Quantity<LengthUnit>(firstQuantityDto.Value, Enum.Parse<LengthUnit>(firstQuantityDto.Unit, true))
                        .Divide(new Quantity<LengthUnit>(secondQuantityDto.Value, Enum.Parse<LengthUnit>(secondQuantityDto.Unit, true))),

        "weight" => new Quantity<WeightUnit>(firstQuantityDto.Value, Enum.Parse<WeightUnit>(firstQuantityDto.Unit, true))
                        .Divide(new Quantity<WeightUnit>(secondQuantityDto.Value, Enum.Parse<WeightUnit>(secondQuantityDto.Unit, true))),

        "volume" => new Quantity<VolumeUnit>(firstQuantityDto.Value, Enum.Parse<VolumeUnit>(firstQuantityDto.Unit, true))
                        .Divide(new Quantity<VolumeUnit>(secondQuantityDto.Value, Enum.Parse<VolumeUnit>(secondQuantityDto.Unit, true))),

        "temperature" => throw new InvalidOperationException("Temperature does not support division"),

        _ => throw new ArgumentException("Unsupported measurement type")
      };
    }
  }
}