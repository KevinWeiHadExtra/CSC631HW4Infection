package networking.request;

// Java Imports
import java.io.IOException;

// Other Imports
import model.Player;
import networking.response.ResponseConfirm;
import core.NetworkManager;

public class RequestConfirm extends GameRequest {

    // Responses
    private ResponseConfirm responseConfirm;

    public RequestConfirm() {
        responses.add(responseConfirm = new ResponseConfirm());
    }

    @Override
    public void parse() throws IOException {
    
    }

    @Override
    public void doBusiness() throws Exception {
        Player player = client.getPlayer();

        responseConfirm.setPlayer(player);

        NetworkManager.addResponseForAllOnlinePlayers(player.getID(), responseConfirm);
    }
}