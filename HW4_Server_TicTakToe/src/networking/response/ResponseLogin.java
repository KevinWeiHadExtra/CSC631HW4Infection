package networking.response;

// Other Imports
import core.GameServer;
import metadata.Constants;
import model.Player;
import utility.GamePacket;
import java.util.List;

/**
 * The ResponseLogin class contains information about the authentication
 * process.
 */
public class ResponseLogin extends GameResponse {

    private short status;
    private Player player;
    
    public ResponseLogin() {
        responseCode = Constants.SMSG_LOGIN;
    }

    @Override
    public byte[] constructResponseInBytes() {
        GamePacket packet = new GamePacket(responseCode);
        packet.addShort16(status);
        if (status == 0) {
            packet.addInt32(player.getID());

            GameServer gs = GameServer.getInstance();
            List<Player> activePlayers = gs.getActivePlayers(); 

            boolean otherPlayerExists = false;
            for(Player p : activePlayers) {
                if(p.getID() != player.getID()) {
                    packet.addInt32(p.getID());
                    packet.addString(p.getName());
                    otherPlayerExists = true;
                }
            }

            if(!otherPlayerExists) {
                packet.addInt32(0);
                packet.addString("NO OTHER PLAYER CONNECTED");
                packet.addBoolean(false);
            }
        }
        return packet.getBytes();
    }

    public void setStatus(short status) {
        this.status = status;
    }

    public void setPlayer(Player player) {
        this.player = player;
    }
}