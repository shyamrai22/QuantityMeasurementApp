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
      try
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

        repository.Save(new QuantityMeasurementEntity(
          "COMPARE",
          $"{firstQuantityDto.Value} {firstQuantityDto.Unit}",
          $"{secondQuantityDto.Value} {secondQuantityDto.Unit}",
          operationResult.ToString(),
          firstQuantityDto.MeasurementType
        ));

        return operationResult;
      }
      catch (Exception ex)
      {
        repository.Save(new QuantityMeasurementEntity(
          "COMPARE",
          $"{firstQuantityDto.Value} {firstQuantityDto.Unit}",
          $"{secondQuantityDto.Value} {secondQuantityDto.Unit}",
          null,
          firstQuantityDto.MeasurementType,
          true,
          ex.Message
        ));
        throw;
      }
    }

    public QuantityDTO Convert(QuantityDTO sourceQuantityDto, string targetUnit)
    {
      try
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

        repository.Save(new QuantityMeasurementEntity(
          "CONVERT",
          $"{sourceQuantityDto.Value} {sourceQuantityDto.Unit}",
          null,
          $"{resultDto.Value} {resultDto.Unit}",
          sourceQuantityDto.MeasurementType
        ));

        return resultDto;
      }
      catch (Exception ex)
      {
        repository.Save(new QuantityMeasurementEntity(
          "CONVERT",
          $"{sourceQuantityDto.Value} {sourceQuantityDto.Unit}",
          null,
          null,
          sourceQuantityDto.MeasurementType,
          true,
          ex.Message
        ));
        throw;
      }
    }

    public QuantityDTO Add(QuantityDTO firstQuantityDto, QuantityDTO secondQuantityDto)
    {
      try
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

        repository.Save(new QuantityMeasurementEntity(
          "ADD",
          $"{firstQuantityDto.Value} {firstQuantityDto.Unit}",
          $"{secondQuantityDto.Value} {secondQuantityDto.Unit}",
          $"{resultDto.Value} {resultDto.Unit}",
          firstQuantityDto.MeasurementType
        ));

        return resultDto;
      }
      catch (Exception ex)
      {
        repository.Save(new QuantityMeasurementEntity(
          "ADD",
          $"{firstQuantityDto.Value} {firstQuantityDto.Unit}",
          $"{secondQuantityDto.Value} {secondQuantityDto.Unit}",
          null,
          firstQuantityDto.MeasurementType,
          true,
          ex.Message
        ));
        throw;
      }
    }

    public QuantityDTO Subtract(QuantityDTO firstQuantityDto, QuantityDTO secondQuantityDto)
    {
      try
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

        repository.Save(new QuantityMeasurementEntity(
          "SUBTRACT",
          $"{firstQuantityDto.Value} {firstQuantityDto.Unit}",
          $"{secondQuantityDto.Value} {secondQuantityDto.Unit}",
          $"{resultDto.Value} {resultDto.Unit}",
          firstQuantityDto.MeasurementType
        ));

        return resultDto;
      }
      catch (Exception ex)
      {
        repository.Save(new QuantityMeasurementEntity(
          "SUBTRACT",
          $"{firstQuantityDto.Value} {firstQuantityDto.Unit}",
          $"{secondQuantityDto.Value} {secondQuantityDto.Unit}",
          null,
          firstQuantityDto.MeasurementType,
          true,
          ex.Message
        ));
        throw;
      }
    }

    public double Divide(QuantityDTO firstQuantityDto, QuantityDTO secondQuantityDto)
    {
      try
      {
        double result = firstQuantityDto.MeasurementType.ToLower() switch
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

        repository.Save(new QuantityMeasurementEntity(
          "DIVIDE",
          $"{firstQuantityDto.Value} {firstQuantityDto.Unit}",
          $"{secondQuantityDto.Value} {secondQuantityDto.Unit}",
          result.ToString(),
          firstQuantityDto.MeasurementType
        ));

        return result;
      }
      catch (Exception ex)
      {
        repository.Save(new QuantityMeasurementEntity(
          "DIVIDE",
          $"{firstQuantityDto.Value} {firstQuantityDto.Unit}",
          $"{secondQuantityDto.Value} {secondQuantityDto.Unit}",
          null,
          firstQuantityDto.MeasurementType,
          true,
          ex.Message
        ));
        throw;
      }
    }
  }
}