using AppPurchases.Application.DTOs;

namespace AppPurchases.Tests.Stubs
{
    public class RegisterStub
    {
        public RegisterDTO GetRegisterDTO() =>
            new()
            {
                Id = "1",
                AddressClient = "Rua teste",
                CpfClient = "23123123",
                CreditCard = new List<CreditCardDTO>
                {
                    new CreditCardDTO
                    {
                        CpfClient = "23123123",
                        CreditCardType = Shared.Enuns.CreditCardEnum.Primary,
                        Flag = "Teste",
                        Id = "1",
                        NumberCard = "23423423",
                        SecutiryCode = 10,
                        Validate = "12/25"
                    }
                },
                DateBirthClient = "31/01/1995",
                Gender = Shared.Enuns.GenderEnum.Other,
                NameClient = "Teste",
                Password = "1234"
            };
    }
}
