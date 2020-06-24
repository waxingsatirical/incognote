using incognote.server;

namespace incognote.sticklets
{
    public class RoomForSticklets
    {
        private readonly IRoom room;

        public RoomForSticklets(
            IRoom room
            )
        {
            this.room = room;
        }

        public void StartGame(string connectionId)
        {
            room.ToGroup("New game started.");
        }

        public void SubmitGuess(string connectionId, int guess)
        {

        }
    }
}
