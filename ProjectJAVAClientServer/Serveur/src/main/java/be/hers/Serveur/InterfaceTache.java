package be.hers.Serveur;

import be.hers.Serveur.POJO.Collaborateur;
import be.hers.Serveur.POJO.Tache;

public interface InterfaceTache {
    public String getAllTask();
    public void createCollaborateur(Collaborateur c);
    public void createTask(Tache c);
    public void addPeriodeTravail(int IDTache, String nom, String prenom);
    public void pausePeriodeTravail(int IDTache, String nom, String prenom);
    public void clotureTache(int IDTache, String nom, String prenom);
    public void deleteTache(int IDTache, String nom, String prenom);
    public void annuleTache(int IDTache, String nom, String prenom);
    public void updateDelai();
}
