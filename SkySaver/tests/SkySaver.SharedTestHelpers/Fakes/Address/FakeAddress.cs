namespace SkySaver.SharedTestHelpers.Fakes.Address;

using SkySaver.Domain.Addresses.Dtos;
using SkySaver.Domain.Addresses;

public class FakeAddress
{
    public static Address Generate(AddressForCreationDto addressForCreationDto)
    {
        return new Address(addressForCreationDto.Line1,
            addressForCreationDto.Line2,
            addressForCreationDto.City,
            addressForCreationDto.State,
            addressForCreationDto.PostalCode,
            addressForCreationDto.Country);
    }

    public static Address Generate()
    {
        return Generate(new FakeAddressForCreationDto().Generate());
    }
}