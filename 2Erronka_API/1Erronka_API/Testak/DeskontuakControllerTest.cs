using System;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NHibernate;
using Xunit;
using _1Erronka_API.Controllers;
using _1Erronka_API.DTOak;
using _1Erronka_API.Modeloak;

namespace _1Erronka_API.Testak
{
    public class DeskontuakControllerTest
    {
        private readonly ISessionFactory _dummyFactory;

        public DeskontuakControllerTest()
        {
            var factoryMock = new Mock<ISessionFactory>();
            factoryMock.Setup(f => f.GetCurrentSession()).Returns(new Mock<NHibernate.ISession>().Object);
            _dummyFactory = factoryMock.Object;
        }

        [Fact]
        public void Upsert_KodeaHutsikDenean_BadRequestItzuli()
        {
            var repoMock = new Mock<_1Erronka_API.Repositorioak.DeskontuaRepository>(MockBehavior.Loose, _dummyFactory);
            var controller = new DeskontuakController(repoMock.Object);
            var dto = new DeskontuaUpsertDto { Kodea = "   ", Mota = "ehunekoa", Balioa = 10, Aktibo = true };

            var result = controller.Upsert(dto);

            var bad = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Kodea beharrezkoa da", bad.Value);
        }

        [Fact]
        public void Upsert_MotaHutsikDenean_BadRequestItzuli()
        {
            var repoMock = new Mock<_1Erronka_API.Repositorioak.DeskontuaRepository>(MockBehavior.Loose, _dummyFactory);
            var controller = new DeskontuakController(repoMock.Object);
            var dto = new DeskontuaUpsertDto { Kodea = "K1", Mota = "   ", Balioa = 10, Aktibo = true };

            var result = controller.Upsert(dto);

            var bad = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Mota beharrezkoa da", bad.Value);
        }

        [Fact]
        public void Upsert_ExistitzenEzDenean_BerriaSortuEtaSaveOrUpdateDeitu()
        {
            var repoMock = new Mock<_1Erronka_API.Repositorioak.DeskontuaRepository>(MockBehavior.Strict, _dummyFactory);
            repoMock.Setup(r => r.GetByKodea("K1")).Returns((Deskontua?)null);
            Deskontua? captured = null;
            repoMock.Setup(r => r.SaveOrUpdate(It.IsAny<Deskontua>()))
                .Callback<Deskontua>(d =>
                {
                    captured = d;
                    d.Id = 5;
                });

            var controller = new DeskontuakController(repoMock.Object);
            var dto = new DeskontuaUpsertDto { Kodea = " K1 ", Mota = " ehunekoa ", Balioa = 10, Aktibo = true };

            var result = controller.Upsert(dto);

            var ok = Assert.IsType<OkObjectResult>(result);
            dynamic body = ok.Value!;
            Assert.True((bool)body.Ok);
            Assert.Equal(5, (int)body.Id);
            Assert.Equal("K1", (string)body.Kodea);

            Assert.NotNull(captured);
            Assert.Equal("K1", captured!.Kodea);
            Assert.Equal("ehunekoa", captured.Mota);
            Assert.Equal(10, captured.Balioa);
            Assert.True(captured.Aktibo);
        }

        [Fact]
        public void Upsert_ExistitzenDenean_EguneratuEtaSaveOrUpdateDeitu()
        {
            var repoMock = new Mock<_1Erronka_API.Repositorioak.DeskontuaRepository>(MockBehavior.Strict, _dummyFactory);
            var existing = new Deskontua { Id = 9, Kodea = "K1", Mota = "zaharra", Balioa = 1, Aktibo = false };
            repoMock.Setup(r => r.GetByKodea("K1")).Returns(existing);
            Deskontua? captured = null;
            repoMock.Setup(r => r.SaveOrUpdate(It.IsAny<Deskontua>())).Callback<Deskontua>(d => captured = d);

            var controller = new DeskontuakController(repoMock.Object);
            var dto = new DeskontuaUpsertDto { Kodea = "K1", Mota = "kopurua", Balioa = 7.5, Aktibo = true };

            var result = controller.Upsert(dto);

            Assert.IsType<OkObjectResult>(result);
            Assert.Same(existing, captured);
            Assert.Equal("kopurua", existing.Mota);
            Assert.Equal(7.5, existing.Balioa);
            Assert.True(existing.Aktibo);
        }

        [Fact]
        public void Upsert_SalbuespenaGertatzean_BadRequestItzuli()
        {
            var repoMock = new Mock<_1Erronka_API.Repositorioak.DeskontuaRepository>(MockBehavior.Strict, _dummyFactory);
            repoMock.Setup(r => r.GetByKodea("K1")).Throws(new Exception("boom"));

            var controller = new DeskontuakController(repoMock.Object);
            var dto = new DeskontuaUpsertDto { Kodea = "K1", Mota = "ehunekoa", Balioa = 10, Aktibo = true };

            var result = controller.Upsert(dto);

            var bad = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("boom", bad.Value);
        }
    }
}
