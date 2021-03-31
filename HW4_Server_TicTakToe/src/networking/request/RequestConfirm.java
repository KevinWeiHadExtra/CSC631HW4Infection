package networking.request;

// Java Imports
import java.io.IOException;

// Other Imports
import model.Player;
import networking.response.ResponseConfirm;
import core.NetworkManager;

public class RequestConfirm extends GameRequest {

    // Responses
    private ResponseConfirm responseReady;

    public RequestConfirm() {
        responses.add(responseReady = new ResponseConfirm());
    }

    @Override
    public void parse() throws IOException {
    
    }

    @Override
    public void doBusiness() throws Exception {
        Player player = client.getPlayer();

        responseReady.setPlayer(player);

        NetworkManager.addResponseForAllOnlinePlayers(player.getID(), responseReady);
    }
}