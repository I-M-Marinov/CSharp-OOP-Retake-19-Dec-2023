using System.Reflection;

namespace Television.Tests
{
    using System;
    using NUnit.Framework;
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SwitchOn_ShouldReturnCorrectMessage()
        {
            var television = new TelevisionDevice("LG", 600, 1920, 1080);
            var result = television.SwitchOn();
            Assert.AreEqual($"Cahnnel {television.CurrentChannel} - Volume {television.Volume} - Sound On", result);
        }

        [Test]
        public void ChangeChannel_WithValidChannel_ShouldUpdateCurrentChannel()
        {
            var television = new TelevisionDevice("Samsung", 1000, 1920, 1080);
            var result = television.ChangeChannel(5);
            Assert.AreEqual(5, result);
        }

        [Test]
        
        public void ChangeChannel_WithInvalidChannel_ShouldThrowAnArgumentException()
        {
            var television = new TelevisionDevice("VelikoTyrnovo", 500, 1920, 1080);
            ArgumentException exception = Assert.Throws<ArgumentException>(() => television.ChangeChannel(-1));
            Assert.AreEqual("Invalid key!", exception.Message);

        }

        [Test]
        public void VolumeChange_WithValidDirectionAndUnits_ShouldUpdateVolume()
        {
            var television = new TelevisionDevice("Tesla", 820, 1920, 1080);
            var result = television.VolumeChange("UP", 10);
            Assert.AreEqual($"Volume: {television.Volume}", result);
        }

        [Test]

        public void VolumeChange_WithMoreThan100ShouldUpdate_VolumeTo100()
        {
            var television = new TelevisionDevice("Pravec", 50, 600, 800);
            var result = television.VolumeChange("UP", 110);
            Assert.AreEqual($"Volume: 100", result);
        }


        [Test]

        public void VolumeChange_WithLesThan0_ShouldReturnVolume0()
        {
            var television = new TelevisionDevice("Pravec2", 50, 600, 800);
            var result = television.VolumeChange("DOWN", 14);
            Assert.AreEqual($"Volume: 0", result);
        }

        [Test]
        public void MuteDevice_WhenNotMuted_ShouldMuteTheDevice()
        {
            var television = new TelevisionDevice("Candy", 900, 1920, 1080);
            var result = television.MuteDevice();
            Assert.AreEqual(true, result);
        }

        [Test]
        public void MuteDevice_WhenAlreadyMuted_ShouldUnmuteTheDevice()
        {
            var television = new TelevisionDevice("Dell", 500, 1920, 1080);
            television.MuteDevice();
            var result = television.MuteDevice();
            Assert.AreEqual(false, result);
        }

        [Test]
        public void ToString_Should_Return_The_CorrectString()
        {
            var television = new TelevisionDevice("Lenovo", 500, 1920, 1080);
            var result = television.ToString();
            Assert.AreEqual($"TV Device: {television.Brand}, Screen Resolution: {television.ScreenWidth}x{television.ScreenHeigth}, Price {television.Price}$", result);
        }
    }
}
    
