using System.Runtime.Serialization;

namespace BackEnd.Common.Core.Dto
{
    [DataContract]
    public class EnumResponseDto
    {
        public EnumResponseDto()
        {
            
        }

        public EnumResponseDto(int id, string name)
        {
            Id = id;
            Name = name;
        }
        [DataMember]public int Id { get; set; }
        [DataMember] public string Name { get; set; }
    }
}