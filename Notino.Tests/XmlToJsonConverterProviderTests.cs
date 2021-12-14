using FluentAssertions;
using Notino.Converters;
using Xunit;

namespace Notino.Tests
{
    public class XmlToJsonConverterProviderTests
    {
        [Fact]
        public void GivenValidXmlString_ConvertFromXmlToJson_ValidJsonIsReturned()
        {
            var sampleXml = @"<?xml version=""1.0"" encoding=""utf-16""?>
                        <myData xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                          <title>Example Title</title>
                          <text>Example Text</text>
                        </myData>";
            var expectedJson = @"{""Title"":""Example Title"",""Text"":""Example Text""}";

            var converter = new XmlToJsonConverterProvider();
            var result = converter.Convert(sampleXml);

            result.Succeeded.Should().BeTrue();
            result.Data.Should().BeEquivalentTo(expectedJson);
        }

        [Fact]
        public void GivenInvalidXmlString_ConvertFromXmlToJson_ErrorIsReturned()
        {
            var sampleXml = @"<?
                          <title>Example Title</title>
                          <text>Example Text</text>
                        </";

            var converter = new XmlToJsonConverterProvider();
            var result = converter.Convert(sampleXml);

            result.Succeeded.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
        }
    }
}
