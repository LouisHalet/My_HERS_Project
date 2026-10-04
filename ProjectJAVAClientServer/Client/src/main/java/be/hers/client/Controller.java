package be.hers.client;

import javafx.application.Platform;
import javafx.fxml.FXML;
import javafx.scene.control.Label;

import java.io.IOException;
import java.net.URI;
import java.net.http.HttpClient;
import java.net.http.WebSocket;
import java.time.LocalDate;
import java.util.concurrent.CompletionStage;

public class Controller {
    HttpClient client = HttpClient.newHttpClient();
    WebSocket webSocket = null;
    DashBoard d = null;
    String nom = "";
    String prenom = "";
    LigneTache ligneSelected = null;
    Controller(DashBoard d) {
        this.d = d;
        connecte();
    }

    /**
     * Implémente les méthodes Overrider de WebSoket.
     */
    public void connecte() {
        webSocket = client.newWebSocketBuilder()
                .buildAsync(URI.create("ws://localhost:8080/ws"), new WebSocket.Listener() {

                    @Override
                    public void onOpen(WebSocket webSocket1) {
                        System.out.println("du listener" + webSocket1);
                        System.out.println("Connexion ouverte");
                        //webSocket1.sendText("Hello WebSocket! I'm Isabelle", true);
                        WebSocket.Listener.super.onOpen(webSocket1);

                    }

                    @Override
                    public CompletionStage<?> onText(WebSocket webSocket, CharSequence data, boolean last) {
                        System.out.println("Message reçu : " + data);
                        Controller.this.traiterMessage(data);
                        return WebSocket.Listener.super.onText(webSocket, data, last);
                    }

                    @Override
                    public CompletionStage<?> onClose(WebSocket webSocket, int statusCode, String reason) {
                        System.out.println("Connexion fermée : " + reason);
                        return WebSocket.Listener.super.onClose(webSocket, statusCode, reason);
                    }


                    @Override
                    public void onError(WebSocket webSocket, Throwable error) {
                        System.out.println("Erreur : " + error.getMessage());
                    }
                }).join();
    }

    /**
     * Traite le message recu en paramètre en fonction du début du CharSequence.
     * @param data message recu du serveur
     */
    public void traiterMessage(CharSequence data) {
        String[] donnee = data.toString().split("=");
        if (donnee[0].equals("listTask")) {
            Platform.runLater(() -> {
                d.replaceFieldTable(donnee[1]);
            });
        }

    }

    /**
     * envoie un message pour dire qu'il quitte et ferme le socket
     * @throws IOException si la connexion a eu un pbm
     */
    public void quitter()throws IOException{
        webSocket.sendText("quitter:", false);
    }

    /**
     * envoi un message permettant de créer un collaborateur
     * @param nom le nom du collaborateur
     * @param prenom le prenom du collaborateur
     * @throws IOException  si la connexion a eu un pbm
     */
    public void createProfile(String nom,String prenom)throws IOException{
        webSocket.sendText("createProfile:"+nom+";"+prenom, true);
        this.nom = nom;
        this.prenom = prenom;
    }

    /**
     * envoi un message permettant de créer une tache
     * @param description description de la tache
     * @param ld la date d'échéance
     * @param hour l'heure de l'échéance
     * @param minute les minutes de l'échéance
     * @throws IOException si la connexion a eu un pbm
     */
    public void createTask(String description, LocalDate ld, int hour, int minute)throws IOException{
        webSocket.sendText("createTask:"+description+";"+ld.toString()+";"+hour+";"+minute+";"+nom+";"+prenom, true);
    }

    /**
     * envoi un message permettant de travailler sur une tache
     * @throws IOException si la connexion a eu un pbm
     */
    public void work()throws IOException{
        webSocket.sendText("work:"+ligneSelected.getIDTache()+";"+nom+";"+prenom, true);
    }

    /**
     * envoi un message permettant de mettre en pause une tache
     * @throws IOException si la connexion a eu un pbm
     */
    public void pause()throws IOException{
        webSocket.sendText("pause:"+ligneSelected.getIDTache()+";"+nom+";"+prenom, true);
    }

    /**
     * envoi un message permettant de cloturer une tache
     * @throws IOException si la connexion a eu un pbm
     */
    public void cloture()throws IOException{
        webSocket.sendText("cloture:"+ligneSelected.getIDTache()+";"+nom+";"+prenom, true);
    }

    /**
     * envoi un message permettant de supprimer une tache
     * @throws IOException si la connexion a eu un pbm
     */
    public void delete()throws IOException{
        webSocket.sendText("delete:"+ligneSelected.getIDTache()+";"+nom+";"+prenom, true);
    }

    /**
     * envoi un message permettant d'annuler une tache
     * @throws IOException si la connexion a eu un pbm
     */
    public void annule()throws IOException{
        webSocket.sendText("annule:"+ligneSelected.getIDTache()+";"+nom+";"+prenom, true);
    }


}
