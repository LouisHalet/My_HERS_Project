package be.hers.Serveur.DAO;

import be.hers.Serveur.POJO.Collaborateur;
import be.hers.Serveur.POJO.PeriodeTravail;
import oracle.jdbc.OraclePreparedStatement;
import oracle.jdbc.OracleTypes;

import java.sql.*;
import java.time.LocalDateTime;
import java.time.ZoneId;
import java.util.ArrayList;
import java.util.List;

public class DAOPeriodeTravail extends DAO<PeriodeTravail> {
    /**
     * Convertit un LocaldateTime en timestamp
     * @param localDateTime la date à convertir
     * @return un timestamp correspondant au LocalDateTime
     */
    public static Timestamp convertToTimestamp(LocalDateTime localDateTime) {
        return localDateTime == null ? null : Timestamp.valueOf(localDateTime);
    }

    /**
     * cherche une période de travail avec son ID donné
     * @param objectToSearchInDB the identifier of the object to search for in the table.
     * @return une période de travail correspondant à l'id
     * @throws SQLException
     */
    public PeriodeTravail find(int objectToSearchInDB) throws SQLException {
        PreparedStatement prStat = null;
        ResultSet rs = null;
        PeriodeTravail periodeTravailFind = null;


        String query = "SELECT ID, nbrMinutes, FKTache, FKCollaborateur, DateDebut " +
                "FROM PeriodeTravail " +
                "WHERE ID = ?";

        try {
            prStat = connect.prepareStatement(query);
            prStat.setInt(1, objectToSearchInDB);
            rs = prStat.executeQuery();
            DAOTache daoTache = new DAOTache();
            DAOCollaborateur daoCollaborateur = new DAOCollaborateur();
            if(rs.next()) {
                periodeTravailFind = new PeriodeTravail(
                        rs.getInt("ID"),
                        rs.getInt("nbrMinutes"),
                        daoTache.find(rs.getInt("FKTache")),
                        daoCollaborateur.find(rs.getInt("FKCollaborateur")),
                        rs.getTimestamp("DateDebut").toLocalDateTime()
                );
            }
        } finally {
            closeStatementAndResultSet(prStat, rs);
        }
        return periodeTravailFind;
    }

    /**
     * Créer une liste de Période de travail
     * @return La liste de Période de travail
     * @throws SQLException
     */

    public List<PeriodeTravail> findAll() throws SQLException {
        PreparedStatement prStat = null;
        ResultSet rs = null;
        List<PeriodeTravail> periodeTravailFind = new ArrayList<>();
        DAOTache daoTache = new DAOTache();
        DAOCollaborateur daoCollaborateur = new DAOCollaborateur();
        String query = "SELECT ID, nbrMinutes, FKTache, FKCollaborateur, DateDebut " +
                "FROM PeriodeTravail";
        while(rs.next()){
            periodeTravailFind.add(new PeriodeTravail(
                    rs.getInt("ID"),
                    rs.getInt("nbrMinutes"),
                    daoTache.find(rs.getInt("FKTache")),
                    daoCollaborateur.find(rs.getInt("FKCollaborateur")),
                    rs.getTimestamp("DateDebut").toLocalDateTime()
            ));
        }
        return periodeTravailFind;

    }

    /**
     * Insert dans la table PeriodeTravail l'objet en paramètre
     * @param objectToInsertInDB l'objet PeriodeTravail en paramètre
     * @return true si l'insertion c'est bien déroulé, false sinon
     * @throws SQLException
     */
    public boolean create(PeriodeTravail objectToInsertInDB) throws SQLException {
        boolean isInserted = false;
        OraclePreparedStatement prStat = null;
        ResultSet generateID = null;

        String query = "INSERT INTO PeriodeTravail (nbrMinutes, FKTache, FKCollaborateur, DateDebut) " +
                "VALUES (?,?,?,?) " +
                "RETURNING ID INTO ?";

        try {
            prStat = (OraclePreparedStatement)connect.prepareStatement(query);
            prStat.setLong(1, objectToInsertInDB.getNbrMinutes());
            prStat.setInt(2, objectToInsertInDB.getTache().getID());
            prStat.setInt(3, objectToInsertInDB.getCollaborateur().getID());
            prStat.setDate(4,new Date(convertToTimestamp(objectToInsertInDB.getDebut()).getTime()));
            prStat.registerReturnParameter(5, OracleTypes.INTEGER);

            int nbLinesInsert = prStat.executeUpdate();
            if (nbLinesInsert > 0) {
                generateID = prStat.getReturnResultSet();
                if(!generateID.next()) {
                    throw new SQLException("Problème lors de la création d'une Période de travail");
                }
                int IDGenerated = generateID.getInt(1);
                objectToInsertInDB.setID(IDGenerated);

                isInserted = true;
            }
        } finally {
            closeStatementAndResultSet(prStat, generateID);
        }
        return isInserted;
    }

    /**
     * Met à jour l'objet Période de travail en paramètre par rapport à FKTache et FKCollaborateur
     * @param objectToUpdateInDB l'objet Période de travail
     * @return true si l'objet est mis à jour en bd, false sinon
     * @throws SQLException
     */
    public boolean update(PeriodeTravail objectToUpdateInDB) throws SQLException {
        boolean isUpdated = false;
        PreparedStatement prStat = null;

        String query = "UPDATE PeriodeTravail SET  nbrMinutes = ?, DateDebut = ? WHERE FKTache = ? AND FKCollaborateur = ?";

        try{
            prStat = connect.prepareStatement(query);
            prStat.setLong(1, objectToUpdateInDB.getNbrMinutes());
            prStat.setDate(2, new Date(convertToTimestamp(objectToUpdateInDB.getDebut()).getTime()));
            prStat.setInt(3, objectToUpdateInDB.getTache().getID());
            prStat.setInt(4, objectToUpdateInDB.getCollaborateur().getID());
            int nbLinesUpdate = prStat.executeUpdate();
            if(nbLinesUpdate > 0) {
                isUpdated = true;
            }
        } finally {
            closeStatement(prStat);
        }

        return isUpdated;

    }

    /**
     * Supprime l'objet passé en paramètre de la BD
     * @param objectToDeleteFormDB l'objet Période de travail
     * @return true si la ligne est bien supprimé, false sinon
     * @throws SQLException
     */
    public boolean delete(PeriodeTravail objectToDeleteFormDB) throws SQLException {
        boolean isDeleted = false;
        PreparedStatement prStat = null;

        String query = "DELETE FROM PeriodeTravail " +
                "WHERE ID = ?";

        try {
            prStat = connect.prepareStatement(query);
            prStat.setInt(1, objectToDeleteFormDB.getID());

            int nbLinesDelete = prStat.executeUpdate();
            if(nbLinesDelete > 0) {
                isDeleted = true;
            }
        } finally {
            closeStatement(prStat);
        }
        return isDeleted;

    }

    /**
     * Cherche une Periode de Travail lié par FKTache et FKCollaborateur.
     * @param IDTache l'id de la tache
     * @param IDCollaborateur l'id du Collaborateur
     * @return la période de travail lié aux id en paramètre
     * @throws SQLException
     */
    public PeriodeTravail findByTaskAndCollaborateur(int IDTache, int IDCollaborateur) throws SQLException {
        PreparedStatement prStat = null;
        ResultSet rs = null;
        PeriodeTravail periodeTravailFind = null;


        String query = "SELECT ID, nbrMinutes, FKTache, FKCollaborateur, DateDebut " +
                "FROM PeriodeTravail " +
                "WHERE FKTache = ? AND FKCollaborateur = ?";

        try {
            prStat = connect.prepareStatement(query);
            prStat.setInt(1, IDTache);
            prStat.setInt(2, IDCollaborateur);
            rs = prStat.executeQuery();
            DAOTache daoTache = new DAOTache();
            DAOCollaborateur daoCollaborateur = new DAOCollaborateur();
            if(rs.next()) {
                periodeTravailFind = new PeriodeTravail(
                        rs.getInt("ID"),
                        rs.getInt("nbrMinutes"),
                        daoTache.find(rs.getInt("FKTache")),
                        daoCollaborateur.find(rs.getInt("FKCollaborateur")),
                        rs.getTimestamp("DateDebut").toLocalDateTime()
                );
            }
        } finally {
            closeStatementAndResultSet(prStat, rs);
        }
        return periodeTravailFind;
    }

    /**
     * Supprime toutes les périodes de travail lié à l'id de la tache en paramètre
     * @param ID l'id de la tache
     * @return true si les ligne sont supprimés, false sinon
     * @throws SQLException
     */
    public boolean deleteAllWithTaskID(int ID) throws SQLException {
        boolean isDeleted = false;
        PreparedStatement prStat = null;

        String query = "DELETE FROM PeriodeTravail " +
                "WHERE FKTache = ?";

        try {
            prStat = connect.prepareStatement(query);
            prStat.setInt(1, ID);

            int nbLinesDelete = prStat.executeUpdate();
            if(nbLinesDelete > 0) {
                isDeleted = true;
            }
        } finally {
            closeStatement(prStat);
        }
        return isDeleted;

    }
}
