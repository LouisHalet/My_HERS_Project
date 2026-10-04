package be.hers.Serveur.POJO;

import java.util.List;

public class Collaborateur {
    private int ID;
    private String nom;
    private String prenom;

    /**
     * Créer un collaborateur avec tous ses attributs
     * @param ID l'id du collaborateur
     * @param nom le nom du collaborateur
     * @param prenom le prénom du collaborateur
     */
    public Collaborateur(int ID, String nom, String prenom) {
        this.ID = ID;
        this.nom = nom;
        this.prenom = prenom;
    }
    /**
     * Créer un collaborateur sans son ID
     * @param nom le nom du collaborateur
     * @param prenom le prénom du collaborateur
     */
    public Collaborateur(String nom, String prenom) {
        this.nom = nom;
        this.prenom = prenom;
    }

    /**
     *
     * @return le nom du collaborateur
     */
    public String getNom() {
        return nom;
    }

    /**
     * Initialise le nom du collaborateur
     * @param nom le nom du collaborateur
     */
    public void setNom(String nom) {
        this.nom = nom;
    }

    /**
     *
     * @return l'id du collaborateur
     */
    public int getID() {
        return ID;
    }

    /**
     * Initialise l'ID du collaborateur
     * @param ID l'id du collaborateur
     */
    public void setID(int ID) {
        this.ID = ID;
    }

    /**
     *
     * @return le prénom du collaborateur
     */
    public String getPrenom() {
        return prenom;
    }

    /**
     * Initialise le prénom du collaborateur
     * @param prenom le prénom du collaborateur
     */
    public void setPrenom(String prenom) {
        this.prenom = prenom;
    }

}
