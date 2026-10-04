package be.hers.client;

import javafx.application.Application;
import javafx.application.Platform;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.event.EventHandler;
import javafx.fxml.FXMLLoader;
import javafx.geometry.Insets;
import javafx.geometry.Pos;
import javafx.scene.Scene;
import javafx.scene.control.*;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.layout.*;
import javafx.scene.paint.Color;
import javafx.scene.text.Font;
import javafx.stage.Modality;
import javafx.stage.Stage;
import javafx.stage.WindowEvent;

import javax.xml.validation.Schema;
import java.io.IOException;
import java.time.LocalDate;
import java.time.LocalDateTime;
import java.util.FormattableFlags;

public class DashBoard extends Application {
    private Label title = new Label();
    private Controller c;
    private Button ajouterTache = new Button("Ajouter un tache");
    private Button profile = new Button("Profil");
    private Label lbTache = new Label("Tache en cours");
    private Label error = new Label();
    TableView<LigneTache> tableView = new TableView<>();

    public static void main(String[] args){
        launch(args);
    }

    /**
     * Créer la fenêtre à afficher avec tous ses composants
     * @param stage la fenètre
     * @throws IOException si la connexion a eu un pbm
     */
    @Override
    public void start(Stage stage) throws IOException {
        c = new Controller(this);
        BorderPane root ;
        root = new BorderPane();

        ajouterTache.setPrefSize(600, 50);
        root.setPrefSize(1000, 600);
        VBox panel1 = new VBox(15); // le 15 c'est l'espace entre les nodes
        panel1.setVisible(false);
        VBox panel2 = new VBox(15);
        HBox panel3 = new HBox(15);

        TableColumn<LigneTache, String> column2 =
                new TableColumn<>("Description");

        column2.setCellValueFactory(
                new PropertyValueFactory<>("description"));
        TableColumn<LigneTache, String> column3 =
                new TableColumn<>("Echéance");

        column3.setCellValueFactory(
                new PropertyValueFactory<>("echeance"));
        TableColumn<LigneTache, String> column4 =
                new TableColumn<>("Status");

        column4.setCellValueFactory(
                new PropertyValueFactory<>("statusGlobal"));

        TableColumn<LigneTache, String> column5 =
                new TableColumn<>("Créateur");

        column5.setCellValueFactory(
                new PropertyValueFactory<>("createur"));

        TableColumn<LigneTache, String> column6 =
                new TableColumn<>("Travailleur");

        column6.setCellValueFactory(
                new PropertyValueFactory<>("travailleur"));
        tableView.getColumns().addAll(column2,column3,column4,column5,column6);
        tableView.setColumnResizePolicy(TableView.CONSTRAINED_RESIZE_POLICY);
        root.setOnMouseClicked(e -> {
            if (e.getTarget() != tableView) {
                panel1.setVisible(false);
            }
        });
        tableView.setOnMouseClicked(e -> {
            panel1.setVisible(true);
            LigneTache ligne = tableView.getSelectionModel().getSelectedItem();
            if (ligne != null) {
                panel1.setVisible(true);
                c.ligneSelected = ligne;
            }
        });
        lbTache.setFont(new Font("Arial", 24));
        lbTache.setLabelFor(tableView);
        tableView.setPrefSize(650, 300);
        tableView.setMaxSize(650, 300);
        tableView.setRowFactory(tv -> new TableRow<LigneTache>() {
            @Override
            protected void updateItem(LigneTache item, boolean empty) {
                super.updateItem(item, empty);

                if (empty || item == null) {
                    setStyle("");
                }else{
                    switch (item.getStatusTiming()) {
                        case "En retard":
                            setStyle("-fx-background-color: #f8b4b4;");
                            break;
                        case "Intermediaire":
                            setStyle("-fx-background-color: #edd392;");
                            break;
                        case "Dans le délai":
                            setStyle("-fx-background-color: #b7f7c4;");
                            break;
                        default:
                            setStyle("");
                            break;
                    }
                }
            }
        });
        ajouterTache.setOnAction(e -> {
                    if(!c.nom.isEmpty() && !c.prenom.isEmpty())
                        createTache(stage);
                    else
                        createProfile(stage);
        });

        ajouterTache.setFont(new Font("Arial", 15));
        Label legendeRetard = new Label("  En retard  ");
        legendeRetard.setStyle("-fx-background-color: #f8b4b4; -fx-padding: 4 10 4 10; -fx-background-radius: 4; -fx-font-family: Arial; -fx-font-size: 12;");

        Label legendeInter = new Label("  Intermédiaire  ");
        legendeInter.setStyle("-fx-background-color: #edd392; -fx-padding: 4 10 4 10; -fx-background-radius: 4; -fx-font-family: Arial; -fx-font-size: 12;");

        Label legendeDelai = new Label("  Dans le délai  ");
        legendeDelai.setStyle("-fx-background-color: #b7f7c4; -fx-padding: 4 10 4 10; -fx-background-radius: 4; -fx-font-family: Arial; -fx-font-size: 12;");

        panel2.getChildren().addAll(ajouterTache,lbTache,tableView,error,legendeRetard,legendeInter,legendeDelai);
        panel2.setAlignment(Pos.CENTER);
        profile.setOnAction(e -> createProfile(stage));
        Button exit = new Button("Sortir");
        exit.setOnAction((e)-> {try {
            c.quitter();
            Platform.exit();

        } catch (Exception e1) {

            e1.printStackTrace();
        }});
        stage.setOnCloseRequest(new EventHandler<WindowEvent>() {
            public void handle(WindowEvent t) {
                try {
                    c.quitter();
                    Platform.exit();
                } catch (IOException e) {

                    e.printStackTrace();
                }
            }
        });
        panel3.setAlignment(Pos.CENTER_RIGHT);
        panel3.setPadding(new Insets(10));
        exit.setFont(new Font("Arial", 15));
        panel3.getChildren().addAll(profile,exit);

        Button work = new Button("Travailler sur la tâche");
        work.setOnAction(e -> {
            if(c.ligneSelected.getTravailleur().equals(" ")){
                if(!c.nom.isEmpty() && !c.prenom.isEmpty()){
                    try {
                        c.work();
                    } catch (IOException x) {
                        x.printStackTrace();
                    }
                }else{
                    error.setText("Erreur : Pas de personne connectée");
                    createProfile(stage);
                }

            }else{
                error.setText("Erreur : Quelqu'un travail déja sur cette tache");
            }

        });
        work.setFont(new Font("Arial", 15));
        work.setMaxSize(200, 30);
        Button pause = new Button("Mettre en pause la tâche");
        pause.setOnAction(e -> {
            String str = "";
            if(!c.nom.isEmpty() && !c.prenom.isEmpty()) {
                str = c.prenom + " " + c.nom;
                if(c.ligneSelected.getTravailleur().equals(str)) {
                    try {
                        c.pause();
                    } catch (IOException x) {
                        x.printStackTrace();
                    }
                }
                else{
                    error.setText("Erreur : Vous n'êtes pas la personne assigné à cette tache");
                }
            }else{

                error.setText("Erreur : Pas de personne connectée");
                createProfile(stage);
            }
        });
        pause.setFont(new Font("Arial", 15));
        pause.setMaxSize(200, 30);
        Button cloture = new Button("Clôturer la tâche");
        cloture.setOnAction(e -> {
            String str = "";
            if(!c.nom.isEmpty() && !c.prenom.isEmpty()) {
                str = c.prenom + " " + c.nom;
                if(c.ligneSelected.getTravailleur().equals(str)) {
                    try {
                        c.cloture();
                    } catch (IOException x) {
                        x.printStackTrace();
                    }
                }
                else{
                    error.setText("Erreur : Vous n'êtes pas la personne assigné à cette tache");
                }
            }else{
                error.setText("Erreur : Pas de personne connectée");
                createProfile(stage);
            }
        });

        cloture.setFont(new Font("Arial", 15));
        cloture.setMaxSize(200, 30);
        Button delete = new Button("Supprimer la tâche");
        delete.setOnAction(e -> {
            String str = "";
            if(!c.nom.isEmpty() && !c.prenom.isEmpty()) {
                str = c.prenom + " " + c.nom;
                if(c.ligneSelected.getCreateur().equals(str) && c.ligneSelected.getTravailleur().equals(" ")) {
                    try {
                        c.delete();
                    } catch (IOException x) {
                        x.printStackTrace();
                    }
                }
                else{
                    error.setText("Erreur : Vous n'êtes pas le créateur de cette tache ou cette tache est assignée a quelqu'un.");
                }
            }else{
                error.setText("Erreur : Pas de personne connectée");
                createProfile(stage);
            }
        });
        delete.setFont(new Font("Arial", 15));
        delete.setMaxSize(200, 30);
        Button annuler = new Button("Annuler la tâche");
        annuler.setOnAction(e -> {
            String str = "";
            if(!c.nom.isEmpty() && !c.prenom.isEmpty()) {
                str = c.prenom + " " + c.nom;
                if(c.ligneSelected.getCreateur().equals(str) && c.ligneSelected.getTravailleur().equals(" ")) {
                    try {
                        c.annule();
                    } catch (IOException x) {
                        x.printStackTrace();
                    }
                }
                else{
                    error.setText("Erreur : Vous n'êtes pas la personne assigné à cette tache");
                }
            }else{
                error.setText("Erreur : Pas de personne connectée");
                createProfile(stage);
            }
        });
        annuler.setFont(new Font("Arial", 15));
        annuler.setMaxSize(200, 30);
        panel1.getChildren().addAll(work,pause,cloture,delete,annuler);
        panel1.setAlignment(Pos.CENTER_RIGHT);
        panel1.setPadding(new Insets(30));
        root.setRight(panel1);
        root.setCenter(panel2);
        root.setBottom(panel3);

        stage.setTitle("Dashboard");
        stage.setScene(new Scene(root));
        stage.show();
    }

    /**
     * Crée un fenetre popup permettant de créer une tache
     * @param parentStage fenetre parent
     */
    private void createTache(Stage parentStage) {
        Stage popupCreate = new Stage();
        popupCreate.initOwner(parentStage);
        popupCreate.initModality(Modality.APPLICATION_MODAL);
        popupCreate.setTitle("Nouvelle tâche");

        Label labelDesc = new Label("Description :");
        TextField champDesc = new TextField();
        champDesc.setMaxWidth(200);
        Label labelEcheance = new Label("Date d'échéance :");
        DatePicker datePicker = new DatePicker();
        datePicker.setMaxWidth(200);
        /*datePicker.setOnAction(e -> {
            LocalDate date = datePicker.getValue();
        });*/
        Label labelHeure = new Label("Heure :");
        Spinner spinnerHour = new Spinner(0, 23, 0);
        spinnerHour.setMaxWidth(200);
        Label labelMinute = new Label("Minute :");
        Spinner spinnerMinute = new Spinner(0, 59, 0);
        spinnerMinute.setMaxWidth(200);
        Button validate = new Button("Ajouter");
        validate.setOnAction(e -> {
            try{
                LocalDate localDate = datePicker.getValue();
                int hour = (int)spinnerHour.getValue();
                int minute = (int)spinnerMinute.getValue();
                //LocalDateTime ldt = localDate.atTime(hour,minute);
                c.createTask(champDesc.getText(),localDate,hour,minute);

            }catch (IOException x){
                x.printStackTrace();
            }
        });
        validate.setMaxWidth(100);
        VBox vbox = new VBox(10,labelDesc,champDesc,labelEcheance,datePicker,labelHeure,spinnerHour,labelMinute,spinnerMinute,validate);
        vbox.setAlignment(Pos.CENTER);
        popupCreate.setScene(new Scene(vbox, 300, 400));
        popupCreate.showAndWait();

    }
    /**
     * Crée un fenetre popup permettant de créer un collaborateur
     * @param parentStage fenetre parent
     */
    private void createProfile(Stage parentStage) {
        Stage popupCreate = new Stage();
        popupCreate.initOwner(parentStage);
        popupCreate.initModality(Modality.APPLICATION_MODAL);
        popupCreate.setTitle("Profil");

        Label labelNom = new Label("Nom :");
        TextField Nom = new TextField();
        Nom.setMaxWidth(200);
        Label labelPrenom = new Label("Prenom :");
        TextField Prenom = new TextField();
        Button validate = new Button("Enregistrer");
        validate.setOnAction(e -> {
            try{
                c.createProfile(Nom.getText(),Prenom.getText());
                profile.setVisible(false);

            }catch (IOException x){
                x.printStackTrace();
            }
        });
        Prenom.setMaxWidth(100);
        VBox vbox = new VBox(10,labelNom,Nom,labelPrenom,Prenom,validate);
        vbox.setAlignment(Pos.CENTER);
        popupCreate.setScene(new Scene(vbox, 300, 400));
        popupCreate.showAndWait();

    }

    /**
     * transforme le message en ligne du tableau.
     * @param data le message à transformer
     */
    public void replaceFieldTable(String data){
        tableView.getItems().clear();
        String[] tab = data.split("/");
        for(int i = 0; i< tab.length;i++){
            String[] field = tab[i].split(";");
            tableView.getItems().add(new LigneTache(Integer.valueOf(field[0]),field[1],field[2],field[3],field[4],field[5],field[6]));
        }


    }
}
