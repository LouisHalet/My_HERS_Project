package be.hers.Serveur.DAO;

import be.hers.Serveur.POJO.Collaborateur;
import be.hers.Serveur.POJO.StatusTache;
import be.hers.Serveur.POJO.StatusTimingTache;
import be.hers.Serveur.POJO.Tache;
import oracle.jdbc.OraclePreparedStatement;
import oracle.jdbc.OracleTypes;

import java.sql.*;
import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;


public class DAOTache extends DAO<Tache> {
    /**
     * Convertit un LocaldateTime en timestamp
     * @param localDateTime la date à convertir
     * @return un timestamp correspondant au LocalDateTime
     */
    public static Timestamp convertToTimestamp(LocalDateTime localDateTime) {
        return localDateTime == null ? null : Timestamp.valueOf(localDateTime);
    }
    /**
     * cherche une Tache avec son ID donné
     * @param objectToSearchInDB the identifier of the object to search for in the table.
     * @return une Tache correspondant à l'id
     * @throws SQLException
     */
    public Tache find(int objectToSearchInDB) throws SQLException{
        PreparedStatement prStat = null;
        ResultSet rs = null;
        Tache tacheFind = null;

        String query = "SELECT ID, Description, Echeance, StatusGlobal, StatusTiming, FKCollaborateur, FKTravailleur " +
                "FROM Tache " +
                "WHERE ID = ?";

        try {
            prStat = connect.prepareStatement(query);
            prStat.setInt(1, objectToSearchInDB);
            rs = prStat.executeQuery();
            DAOCollaborateur daoCollaborateur = new DAOCollaborateur();
            Collaborateur t = null;
            if(rs.next()) {
                LocalDateTime l = rs.getTimestamp("Echeance").toLocalDateTime();
                if(rs.getObject("FKTravailleur") != null)
                    t = daoCollaborateur.find(rs.getInt("FKTravailleur"));
                tacheFind = new Tache(
                        rs.getInt("ID"),
                        rs.getString("Description"),
                        l,
                        StatusTache.fromLabel(rs.getString("StatusGlobal")),
                        StatusTimingTache.fromLabel(rs.getString("StatusTiming")),
                        daoCollaborateur.find(rs.getInt("FKCollaborateur")),
                        t
                );
            }
        } finally {
            closeStatementAndResultSet(prStat, rs);
        }
        return tacheFind;
    }

    /**
     * Créer une liste de Tache
     * @return La liste de Tache
     * @throws SQLException
     */
    public List<Tache> findAll() throws SQLException {
        PreparedStatement prStat = null;
        ResultSet rs = null;
        List<Tache> tacheFind = new ArrayList<>();

        String query = "SELECT ID, Description, Echeance, StatusGlobal, StatusTiming, FKCollaborateur, FKTravailleur " +
                "FROM Tache ";
        DAOCollaborateur daoCollaborateur = new DAOCollaborateur();
        prStat = connect.prepareStatement(query);
        try {
            rs = prStat.executeQuery();

            while (rs.next()) {
                Collaborateur t = null;
                LocalDateTime l = rs.getTimestamp("Echeance").toLocalDateTime();
                if(rs.getObject("FKTravailleur") != null)
                    t = daoCollaborateur.find(rs.getInt("FKTravailleur"));
                tacheFind.add(new Tache(
                        rs.getInt("ID"),
                        rs.getString("Description"),
                        l,
                        StatusTache.fromLabel(rs.getString("StatusGlobal")),
                        StatusTimingTache.fromLabel(rs.getString("StatusTiming")),
                        daoCollaborateur.find(rs.getInt("FKCollaborateur")),
                        t

                ));
            }
        }finally {
            closeStatementAndResultSet(prStat, rs);
        }
        return  tacheFind;

    }

    /**
     * Insert dans la table Tache l'objet en paramètre
     * @param objectToInsertInDB l'objet Tache en paramètre
     * @return true si l'insertion c'est bien déroulé, false sinon
     * @throws SQLException
     */
    public boolean create(Tache objectToInsertInDB) throws SQLException {
        boolean isInserted = false;
        OraclePreparedStatement prStat = null;
        ResultSet generateID = null;

        String query = "INSERT INTO Tache (Description, Echeance, StatusGlobal, StatusTiming, FKCollaborateur, FKTravailleur) " +
                "VALUES (?,?,?,?,?,?) " +
                "RETURNING ID INTO ?";

        try {
            prStat = (OraclePreparedStatement)connect.prepareStatement(query);
            prStat.setString(1, objectToInsertInDB.getDescription());
            prStat.setDate(2, new Date(convertToTimestamp(objectToInsertInDB.getEcheance()).getTime()) );
            prStat.setString(3, objectToInsertInDB.getStatusGlobal().getLabel());
            prStat.setString(4, objectToInsertInDB.getStatusTiming().getLabel());
            prStat.setInt(5, objectToInsertInDB.getCreateur().getID());
            if (objectToInsertInDB.getTravailleur() == null) prStat.setNull(6, java.sql.Types.INTEGER);
            else prStat.setInt(6, objectToInsertInDB.getTravailleur().getID());
            prStat.registerReturnParameter(7, OracleTypes.INTEGER);

            int nbLinesInsert = prStat.executeUpdate();
            if (nbLinesInsert > 0) {
                generateID = prStat.getReturnResultSet();
                if(!generateID.next()) {
                    throw new SQLException("Problème lors de la création d'une Tache");
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
     * Met à jour l'objet Tache en paramètre par rapport à son ID
     * @param objectToUpdateInDB l'objet Tache
     * @return true si l'objet est mis à jour en bd, false sinon
     * @throws SQLException
     */
    public boolean update(Tache objectToUpdateInDB) throws SQLException {
        boolean isUpdated = false;
        PreparedStatement prStat = null;

        String query = "UPDATE Tache " +
                "SET  StatusGlobal = ?, StatusTiming = ?, FKTravailleur = ? " +
                "WHERE ID = ?";

        try{
            prStat = connect.prepareStatement(query);
            prStat.setString(1, objectToUpdateInDB.getStatusGlobal().getLabel());
            prStat.setString(2, objectToUpdateInDB.getStatusTiming().getLabel());
            if (objectToUpdateInDB.getTravailleur() == null) prStat.setNull(3, java.sql.Types.INTEGER);
            else prStat.setInt(3, objectToUpdateInDB.getTravailleur().getID());
            prStat.setInt(4, objectToUpdateInDB.getID());

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
     * @param objectToDeleteFormDB l'objet Tache
     * @return true si la ligne est bien supprimé, false sinon
     * @throws SQLException
     */
    public boolean delete(Tache objectToDeleteFormDB) throws SQLException {
        boolean isDeleted = false;
        PreparedStatement prStat = null;

        String query = "DELETE FROM Tache " +
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

}
