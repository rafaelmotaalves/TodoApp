namespace Api.Controllers.Dtos
{
    public class CardDto {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ColumnDto {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<CardDto> Cards { get; set; }
    }

    public class BoardWithColumnsDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<ColumnDto> Columns { get; set; }
    }


}