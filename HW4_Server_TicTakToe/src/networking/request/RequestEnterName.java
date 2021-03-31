package networking.request;

// Java Imports
import java.io.IOException;

// Other Imports
import model.Player;
import networking.response.ResponseEnterName;
import utility.DataReader;
import core.NetworkManager;

public class RequestEnterName extends GameRequest {
    // Data
    private String name;

    // Responses
    private ResponseEnterName responseEnterName;

    public RequestEnterName() {
        responses.add(responseEnterName = new ResponseEnterName());
    }

    @Override
    public void parse() throws IOException {
        name = DataReader.readString(dataInput).trim();
    }

    @Override
    public void doBusiness() throws Exception {
        Player player = client.getPlayer();
       
        player.setName(name);
        responseEnterName.setPlayer(player);

        NetworkManager.addResponseForAllOnlinePlayers(player.getID(), responseEnterName);
    }
}