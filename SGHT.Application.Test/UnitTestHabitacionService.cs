
using Microsoft.Extensions.Logging;
using Moq;
using SGHT.Application.Services;
using SGHT.Persistance.Interfaces;

namespace SGHT.Application.Test
{
    public class UnitTestHabitacionService
    {
        private readonly Mock<IHabitacionRepository> _mockRepository;
        private readonly Mock<ILogger<HabitacionService>> _mockLogger;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly HabitacionService _habitacionService;

        public UnitTestHabitacionService()
        {
            _mockRepository = new Mock<IHabitacionRepository>(MockBehavior.Strict);
            _mockLogger = new Mock<ILogger<HabitacionService>>();
            _mockConfiguration = new Mock<IConfiguration>();

            _habitacionService = new HabitacionService(
                _mockRepository.Object,
                _mockLogger.Object,
                _mockConfiguration.Object
            );
        }

        public void Dispose()
        {
            _mockRepository.Reset();
            _mockLogger.Reset();
            _mockConfiguration.Reset();
        }
    }
}
