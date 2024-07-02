using AutoMapper;

namespace SlotGameServer.Common.Mapping
{
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile);
    }
}
