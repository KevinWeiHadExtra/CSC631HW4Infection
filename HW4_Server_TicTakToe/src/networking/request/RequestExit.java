package networking.request;

// Java Imports
import java.io.IOException;

// Other Imports
import model.Player;
import networking.response.ResponseExit;
import core.NetworkManager;

public class RequestExit extends GameRequest {
    // Responses
    private ResponseExit responseLeave;

    public RequestExit() {
        responses.add(responseLeave = new ResponseExit());
    }

    @Override
    public void parse() throws IOException {
    
    }

    @Override
    public void doBusiness() throws Exception {
        Player player = client.getPlayer();

        responseLeave.setPlayer(player);

        NetworkManager.addResponseForAllOnlinePlayers(player.getID(), responseLeave);
    }
}