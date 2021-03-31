package networking.response;

// Other Imports
import metadata.Constants;
import model.Player;
import utility.GamePacket;
import utility.Log;
/**
 * The ResponseLogin class contains information about the authentication
 * process.
 */
public class ResponseConfirm extends GameResponse {
    private Player player;

    public ResponseConfirm() {
        responseCode = Constants.SMSG_CONFIRM;
    }

    @Override
    public byte[] constructResponseInBytes() {
        GamePacket packet = new GamePacket(responseCode);
        packet.addInt32(player.getID());

        Log.printf("Player with id %d is ready", player.getID());
        return packet.getBytes();
    }

    public void setPlayer(Player player) {
        this.player = player;
    }
}