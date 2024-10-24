using Snai3i_BLL.DTO.CraftDto;
using Snai3i_BLL.DTO.ToolsDTO;
using Snai3i_BLL.DTO.WorkerDto;


namespace Snai3i_MVC.ViewModels
{
    public class ModelForIndex
    {
        public IEnumerable<ReadToolDTO> Tools { get; set; }

        public IEnumerable<CraftReadDTO> Crafts { get; set; }

        //public IEnumerable<WorkerReadDto> Workers { get; set; }
    }
}
