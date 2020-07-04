using API.Controllers;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace API.Test
{
    public class CalulaJurosControllerTest
    {
        private CalculaJurosController _controller;
        private ITaxaService _service;

        public CalulaJurosControllerTest()
        {
            _service = new TaxaServiceFake();
            _controller = new CalculaJurosController(_service);
        }

        [Fact]
        public void Mes_5_Valor_Inicial_0()
        {
            var result = _controller.Get(0, 5).Result;
            Assert.IsType<OkObjectResult>(result);
            OkObjectResult okResult = result as OkObjectResult;
            var doubleResult = okResult.Value as Nullable<Double>;
            Assert.NotNull(doubleResult);
            Assert.Equal(0.00, doubleResult.Value, 2);
        }

        [Fact]
        public void Mes_9_Valor_Inicial_123_45()
        {
            var result = _controller.Get(123.45, 9).Result;
            Assert.IsType<OkObjectResult>(result);
            OkObjectResult okResult = result as OkObjectResult;
            var doubleResult = okResult.Value as Nullable<Double>;
            Assert.NotNull(doubleResult);
            Assert.Equal(135.01, doubleResult.Value, 2);
        }

        [Fact]
        public void Mes_0_Valor_Inicial_12()
        {
            var result = _controller.Get(12, 0).Result;
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Mes_13_Valor_Inicial_56_90()
        {
            var result = _controller.Get(56.90, 13).Result;
            Assert.IsType<BadRequestObjectResult>(result);
        }


    }
}
