package be.hers.Serveur;

import be.hers.Serveur.POJO.*;
import org.springframework.cglib.core.Local;
import org.springframework.web.socket.CloseStatus;
import org.springframework.web.socket.TextMessage;
import org.springframework.web.socket.WebSocketSession;
import org.springframework.web.socket.handler.TextWebSocketHandler;

import java.io.IOException;
import java.time.LocalDate;
import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;

public class MyWebSocketHandler extends TextWebSocketHandler {
    InterfaceTache service = new ServiceTache();

    private final List<WebSocketSession> sessions = new ArrayList<WebSocketSession>();

    /**
     * Envoi au client le contenu de la liste des tâches lors de la connection entre socket.
     * @param session la session à rajouter à la liste
     * @throws IOException
     */
    @Override
    public void afterConnectionEstablished(WebSocketSession session) throws IOException {
        System.out.println("Client connecté : " + session.getId());
        sessions.add(session);
        service.updateDelai();
        session.sendMessage(new TextMessage("listTask="+service.getAllTask()));
    }

    /**
     * Traite le message du client en fonction du début du message.
     * @param session la session du client
     * @param message message recu
     * @throws IOException
     */
    @Override
    protected void handleTextMessage(WebSocketSession session, TextMessage message) throws IOException {
        //interpretation du message reçu du client
        //pour interagir avec le service en fonction
        System.out.println("message recu coté serveur" + message.getPayload());
        String texte = message.getPayload();
        String[] tab = texte.split(":");
        if (tab[0].equals("createProfile")) {
            String[] field = tab[1].split(";");
            Collaborateur c = new Collaborateur(field[0],field[1]);
            service.createCollaborateur(c);
        } else if (tab[0].equals("createTask")) {
            String[] field = tab[1].split(";");
            LocalDate ld = LocalDate.parse(field[1]);
            LocalDateTime ldt = ld.atTime(Integer.valueOf(field[2]) ,Integer.valueOf(field[3]));
            Collaborateur c = new Collaborateur(field[4],field[5]);
            Tache t = new Tache(field[0],ldt,StatusTache.NON_ENTAME, StatusTimingTache.DANS_LE_DELAI,c);
            service.createTask(t);
            service.updateDelai();
            broadcast("listTask="+service.getAllTask());
        } else if (tab[0].equals("work")) {
            String[] field = tab[1].split(";");
            service.addPeriodeTravail(Integer.parseInt(field[0]),field[1],field[2]);
            service.updateDelai();
            broadcast("listTask="+service.getAllTask());
        } else if (tab[0].equals("pause")) {
            String[] field = tab[1].split(";");
            service.pausePeriodeTravail(Integer.parseInt(field[0]),field[1],field[2]);
            service.updateDelai();
            broadcast("listTask="+service.getAllTask());
        } else if (tab[0].equals("cloture")) {
            String[] field = tab[1].split(";");
            service.clotureTache(Integer.parseInt(field[0]),field[1],field[2]);
            service.updateDelai();
            broadcast("listTask="+service.getAllTask());
        } else if (tab[0].equals("delete")) {
            String[] field = tab[1].split(";");
            service.deleteTache(Integer.parseInt(field[0]),field[1],field[2]);
            service.updateDelai();
            broadcast("listTask="+service.getAllTask());
        } else if (tab[0].equals("annule")) {
            String[] field = tab[1].split(";");
            service.annuleTache(Integer.parseInt(field[0]),field[1],field[2]);
            service.updateDelai();
            broadcast("listTask="+service.getAllTask());
        }

    }

    /**
     * Ferme la connection client-serveur et supprime ce socket de la liste.
     * @param session la session à supprimer et déconnecter
     * @param status
     */
    @Override
    public void afterConnectionClosed(WebSocketSession session, CloseStatus status) {
        sessions.remove(session);
        System.out.println("Client déconnecté : " + session.getId());
    }

    /**
     * Envoi un message à tous les clients de la liste
     * @param message le message à envoyer
     * @throws IOException
     */
    private void broadcast(String message) throws IOException {
        TextMessage textMessage = new TextMessage(message);
        for (WebSocketSession sess : sessions) {
            if (sess.isOpen()) {
                sess.sendMessage(textMessage);
            }
        }
    }


}
