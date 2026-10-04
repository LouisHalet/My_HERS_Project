package be.hers.Serveur;

import be.hers.Serveur.DAO.DAOCollaborateur;
import be.hers.Serveur.DAO.DAOPeriodeTravail;
import be.hers.Serveur.DAO.DAOTache;
import be.hers.Serveur.POJO.*;

import java.sql.SQLException;
import java.time.LocalDateTime;
import java.time.temporal.ChronoUnit;
import java.util.List;
import java.util.stream.Collectors;

public class ServiceTache implements InterfaceTache{
    /**
     * Met à jour les champs StatutTiming en fonction de la date d'échéance.
     */
    @Override
    public void updateDelai() {
        DAOTache daoTache = new DAOTache();
        LocalDateTime now = LocalDateTime.now();
        try{
            List<Tache> list = daoTache.findAll();
            StatusTimingTache status;
            for(Tache t : list){
                long minutesRestantes = ChronoUnit.MINUTES.between(now, t.getEcheance());

                if (minutesRestantes < 0) {
                    status = StatusTimingTache.EN_RETARD;
                } else if (minutesRestantes <= 60) {
                    status = StatusTimingTache.INTERMEDIAIRE;
                } else {
                    status = StatusTimingTache.DANS_LE_DELAI;
                }
                t.setStatusTiming(status);
                daoTache.update(t);
            }
        }catch (SQLException e) {
            e.printStackTrace();
        }

    }

    /**
     * Récupère toutes les taches, sauf les taches annulées
     * @return un string où chaque tache vaut le résultat de la méthode toString et séparer par un /.
     */
    @Override
    public String getAllTask() {
        DAOTache daoTache = new DAOTache();
        String str = "";
        try{
            List<Tache> list = daoTache.findAll();
            List<Tache> filtres = list.stream()
                    .filter(t -> !"Annulée".equals(t.getStatusGlobal().getLabel()))
                    .collect(Collectors.toList());
            str = filtres.stream()
                    .map(Tache::toString)
                    .collect(Collectors.joining("/"));
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return str;
    }

    /**
     * Créer un collaborateur dans la BD.
     * @param c le collaborateur à créer
     */
    @Override
    public void createCollaborateur(Collaborateur c) {
        DAOCollaborateur daoCollaborateur = new DAOCollaborateur();
        try{
            daoCollaborateur.create(c);
        }catch (SQLException e) {
            e.printStackTrace();
        }
    }

    /**
     * Créer une tache dans la BD.
     * @param t la tache à créer
     */
    @Override
    public void createTask(Tache t) {
        DAOCollaborateur daoCollaborateur = new DAOCollaborateur();
        DAOTache daoTache = new DAOTache();
        try{
            t.setCreateur(daoCollaborateur.findByNom(t.getCreateur().getNom(), t.getCreateur().getPrenom()));
            daoTache.create(t);
        }catch (SQLException e) {
            e.printStackTrace();
        }
    }

    /**
     * Ajoute/met à jour une période de travail (en calculant le temps de travail)
     * @param IDTache l'id de la tache
     * @param nom le nom du collaborateur
     * @param prenom le prenom du collaborateur
     */
    @Override
    public void addPeriodeTravail(int IDTache, String nom, String prenom){
        DAOCollaborateur daoCollaborateur = new DAOCollaborateur();
        DAOTache daoTache = new DAOTache();
        DAOPeriodeTravail daoPeriodeTravail = new DAOPeriodeTravail();
        try{
            Collaborateur c = daoCollaborateur.findByNom(nom, prenom);
            Tache t = daoTache.find(IDTache);
            PeriodeTravail pToAdd = new PeriodeTravail(0,t,c, LocalDateTime.now());
            PeriodeTravail pFind = daoPeriodeTravail.findByTaskAndCollaborateur(t.getID(), c.getID());
            if(pFind == null){
                daoPeriodeTravail.create(pToAdd);
            }else{
                long minute = ChronoUnit.MINUTES.between(pFind.getDebut(),pToAdd.getDebut());
                pToAdd.setNbrMinutes(minute);
                daoPeriodeTravail.update(pToAdd);
            }
            t.setTravailleur(c);
            t.setStatusGlobal(StatusTache.EN_COURS);
            daoTache.update(t);
        }catch (SQLException e) {
            e.printStackTrace();
        }
    }

    /**
     * Met en pause une période de travail (en calculant le temps de travail)
     * @param IDTache l'id de la tache
     * @param nom le nom du collaborateur
     * @param prenom le prenom du collaborateur
     */
    @Override
    public void pausePeriodeTravail(int IDTache, String nom, String prenom){
        DAOCollaborateur daoCollaborateur = new DAOCollaborateur();
        DAOTache daoTache = new DAOTache();
        DAOPeriodeTravail daoPeriodeTravail = new DAOPeriodeTravail();
        try{
            Collaborateur c = daoCollaborateur.findByNom(nom, prenom);
            Tache t = daoTache.find(IDTache);
            PeriodeTravail pToAdd = new PeriodeTravail(0,t,c, LocalDateTime.now());
            PeriodeTravail pFind = daoPeriodeTravail.findByTaskAndCollaborateur(t.getID(), c.getID());
            if(pFind == null){
                daoPeriodeTravail.create(pToAdd);
            }else{
                long minute = ChronoUnit.MINUTES.between(pFind.getDebut(),pToAdd.getDebut());
                pToAdd.setNbrMinutes(minute);
                daoPeriodeTravail.update(pToAdd);
            }
            t.setTravailleur(null);
            t.setStatusGlobal(StatusTache.EN_PAUSE);
            daoTache.update(t);
        }catch (SQLException e) {
            e.printStackTrace();
        }
    }

    /**
     * Cloture une tache en mettant à jour le temps de travail du collaborateur sur cette tache
     * @param IDTache l'id de la tache
     * @param nom le nom du collaborateur
     * @param prenom le prenom du collaborateur
     */
    @Override
    public void clotureTache(int IDTache, String nom, String prenom) {
        DAOCollaborateur daoCollaborateur = new DAOCollaborateur();
        DAOTache daoTache = new DAOTache();
        DAOPeriodeTravail daoPeriodeTravail = new DAOPeriodeTravail();
        try{
            Collaborateur c = daoCollaborateur.findByNom(nom, prenom);
            Tache t = daoTache.find(IDTache);
            PeriodeTravail pToAdd = new PeriodeTravail(0,t,c, LocalDateTime.now());
            PeriodeTravail pFind = daoPeriodeTravail.findByTaskAndCollaborateur(t.getID(), c.getID());
            if(pFind == null){
                daoPeriodeTravail.create(pToAdd);
            }else{
                long minute = ChronoUnit.MINUTES.between(pFind.getDebut(),pToAdd.getDebut());
                pToAdd.setNbrMinutes(minute);
                daoPeriodeTravail.update(pToAdd);
            }
            t.setTravailleur(null);
            t.setStatusGlobal(StatusTache.CLOTUREES);
            daoTache.update(t);
        }catch (SQLException e) {
            e.printStackTrace();
        }
    }

    /**
     * Supprime une tache en supprimant également les périodes de travail lié à cette tache
     * @param IDTache l'id de la tache
     * @param nom le nom du collaborateur
     * @param prenom le prenom du collaborateur
     */
    @Override
    public void deleteTache(int IDTache, String nom, String prenom) {
        DAOCollaborateur daoCollaborateur = new DAOCollaborateur();
        DAOTache daoTache = new DAOTache();
        DAOPeriodeTravail daoPeriodeTravail = new DAOPeriodeTravail();
        try{

            Tache t = daoTache.find(IDTache);
            daoPeriodeTravail.deleteAllWithTaskID(IDTache);
            daoTache.delete(t);

        }catch (SQLException e) {
            e.printStackTrace();
        }
    }

    /**
     * Annule la tache donnée par son ID
     * @param IDTache l'id de la tache
     * @param nom le nom du collaborateur
     * @param prenom le prenom du collaborateur
     */
    @Override
    public void annuleTache(int IDTache, String nom, String prenom) {
        DAOCollaborateur daoCollaborateur = new DAOCollaborateur();
        DAOTache daoTache = new DAOTache();
        DAOPeriodeTravail daoPeriodeTravail = new DAOPeriodeTravail();
        try{
            Collaborateur c = daoCollaborateur.findByNom(nom, prenom);
            Tache t = daoTache.find(IDTache);
            PeriodeTravail pToAdd = new PeriodeTravail(0,t,c, LocalDateTime.now());
            PeriodeTravail pFind = daoPeriodeTravail.findByTaskAndCollaborateur(t.getID(), c.getID());
            if(pFind == null){
                daoPeriodeTravail.create(pToAdd);
            }else{
                long minute = ChronoUnit.MINUTES.between(pFind.getDebut(),pToAdd.getDebut());
                pToAdd.setNbrMinutes(minute);
                daoPeriodeTravail.update(pToAdd);
            }
            t.setTravailleur(null);
            t.setStatusGlobal(StatusTache.ANNULEE);
            daoTache.update(t);
        }catch (SQLException e) {
            e.printStackTrace();
        }
    }
}
