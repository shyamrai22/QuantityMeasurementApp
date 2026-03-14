using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Controller;
using QuantityMeasurementApp.Repository;
using QuantityMeasurementApp.Service;
using QuantityMeasurementApp.Model.DTO;

namespace QuantityMeasurementApp.Tests
{
  [TestClass]
  public class QuantityMeasurementServiceTests
  {
    private QuantityMeasurementServiceImpl service;
    private QuantityMeasurementCacheRepository repository;
    private QuantityMeasurementController controller;

    [TestInitialize]
    public void Setup()
    {
      repository = QuantityMeasurementCacheRepository.GetInstance();
      service = new QuantityMeasurementServiceImpl(repository);
      controller = new QuantityMeasurementController(service);
    }

    [TestMethod]
    public void Given1FeetAnd12Inch_WhenCompared_ShouldReturnTrue()
    {
      var firstQuantity = new QuantityDTO(1, "FEET", "Length");
      var secondQuantity = new QuantityDTO(12, "INCH", "Length");

      bool result = service.Compare(firstQuantity, secondQuantity);

      Assert.IsTrue(result);
    }

    [TestMethod]
    public void Given1KgAnd1000Gram_WhenCompared_ShouldReturnTrue()
    {
      var firstQuantity = new QuantityDTO(1, "KILOGRAM", "Weight");
      var secondQuantity = new QuantityDTO(1000, "GRAM", "Weight");

      bool result = service.Compare(firstQuantity, secondQuantity);

      Assert.IsTrue(result);
    }

    [TestMethod]
    public void Given1FeetAnd12Inch_WhenAdded_ShouldReturn2Feet()
    {
      var firstQuantity = new QuantityDTO(1, "FEET", "Length");
      var secondQuantity = new QuantityDTO(12, "INCH", "Length");

      var result = service.Add(firstQuantity, secondQuantity);

      Assert.AreEqual(2, result.Value);
      Assert.AreEqual("FEET", result.Unit);
    }

    [TestMethod]
    public void Given2FeetAnd12Inch_WhenSubtracted_ShouldReturn1Feet()
    {
      var firstQuantity = new QuantityDTO(2, "FEET", "Length");
      var secondQuantity = new QuantityDTO(12, "INCH", "Length");

      var result = service.Subtract(firstQuantity, secondQuantity);

      Assert.AreEqual(1, result.Value);
    }

    [TestMethod]
    public void Given2FeetAnd1Feet_WhenDivided_ShouldReturn2()
    {
      var firstQuantity = new QuantityDTO(2, "FEET", "Length");
      var secondQuantity = new QuantityDTO(1, "FEET", "Length");

      double result = service.Divide(firstQuantity, secondQuantity);

      Assert.AreEqual(2, result);
    }

    [TestMethod]
    public void Given2LitreAnd500Millilitre_WhenAdded_ShouldReturn2Point5Litre()
    {
      var firstQuantity = new QuantityDTO(2, "LITRE", "Volume");
      var secondQuantity = new QuantityDTO(500, "MILLILITRE", "Volume");

      var result = service.Add(firstQuantity, secondQuantity);

      Assert.AreEqual(2.5, result.Value);
    }

    [TestMethod]
    public void TemperatureAddition_ShouldThrowException()
    {
      var firstTemperature = new QuantityDTO(30, "CELSIUS", "Temperature");
      var secondTemperature = new QuantityDTO(40, "CELSIUS", "Temperature");

      Assert.ThrowsException<InvalidOperationException>(() =>
          service.Add(firstTemperature, secondTemperature));
    }

    [TestMethod]
    public void Repository_ShouldStoreMeasurement()
    {
      int countBefore = repository.GetAll().Count;

      var firstQuantity = new QuantityDTO(1, "FEET", "Length");
      var secondQuantity = new QuantityDTO(12, "INCH", "Length");

      service.Compare(firstQuantity, secondQuantity);

      int countAfter = repository.GetAll().Count;

      Assert.IsTrue(countAfter > countBefore);
    }
  }
}