package be.hers.Serveur.DAO;

import be.hers.Serveur.POJO.Collaborateur;
import be.hers.Serveur.POJO.StatusTache;
import be.hers.Serveur.POJO.StatusTimingTache;
import be.hers.Serveur.POJO.Tache;
import be.hers.Serveur.POJO.Collaborateur;
import oracle.jdbc.OraclePreparedStatement;
import oracle.jdbc.OracleTypes;

import java.sql.Date;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;

public class DAOCollaborateur extends DAO<Collaborateur> {
    /**
     * cherche un Collaborateur avec son ID donné
     * @param objectToSearchInDB the identifier of the object to search for in the table.
     * @return un Collaborateur correspondant à l'id
     * @throws SQLException
     */
    public Collaborateur find(int objectToSearchInDB) throws SQLException {
        PreparedStatement prStat = null;

        ResultSet rs = null;
        Collaborateur collaborateurFind = null;

        String query = "SELECT ID, Nom, Prenom " +
                "FROM Collaborateur " +
                "WHERE ID = ?";

        try {
            prStat = connect.prepareStatement(query);
            prStat.setInt(1, objectToSearchInDB);
            rs = prStat.executeQuery();
            if(rs.next()) {
                collaborateurFind = new Collaborateur(
                        rs.getInt("ID"),
                        rs.getString("Nom"),
                        rs.getString("Prenom")
                );
            }
        } finally {
            closeStatementAndResultSet(prStat, rs);
        }
        return collaborateurFind;
    }

    /**
     * Créer une liste de Collaborateur
     * @return La liste de Collaborateur
     * @throws SQLException
     */
    public List<Collaborateur> findAll() throws SQLException {
        PreparedStatement prStat = null;
        ResultSet rs = null;
        List<Collaborateur> collaborateurFind = new ArrayList<>();
        String query = "SELECT ID, Nom, Prenom " +
                "FROM Collaborateur";
        prStat = connect.prepareStatement(query);
        try{
            rs = prStat.executeQuery();
            while(rs.next()){
                collaborateurFind.add(new Collaborateur(
                        rs.getInt("ID"),
                        rs.getString("Nom"),
                        rs.getString("Prenom")
                ));
            }
        }finally {
            closeStatementAndResultSet(prStat, rs);
        }


        return collaborateurFind;

    }
    /**
     * Insert dans la table Collaborateur l'objet en paramètre (l'id est modifié par rapport à la valeur de l'id autogénéré)
     * @param objectToInsertInDB l'objet Collaborateur en paramètre
     * @return true si l'insertion c'est bien déroulé, false sinon
     * @throws SQLException
     */
    public boolean create(Collaborateur objectToInsertInDB) throws SQLException {
        boolean isInserted = false;
        OraclePreparedStatement prStat = null;
        ResultSet generateID = null;

        String query = "INSERT INTO Collaborateur (Nom, Prenom) " +
                "VALUES (?,?) " +
                "RETURNING ID INTO ?";

        try {
            prStat = (OraclePreparedStatement)connect.prepareStatement(query);
            prStat.setString(1, objectToInsertInDB.getNom());
            prStat.setString(2, objectToInsertInDB.getPrenom());

            prStat.registerReturnParameter(3, OracleTypes.INTEGER);

            int nbLinesInsert = prStat.executeUpdate();
            if (nbLinesInsert > 0) {
                generateID = prStat.getReturnResultSet();
                if(!generateID.next()) {
                    throw new SQLException("Problème lors de la création d'un Collaborateur");
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
     * Met à jour l'objet Collaborateur en paramètre par rapport à son ID
     * @param objectToUpdateInDB l'objet Collaborateur
     * @return true si l'objet est mis à jour en bd, false sinon
     * @throws SQLException
     */
    public boolean update(Collaborateur objectToUpdateInDB) throws SQLException {
        boolean isUpdated = false;
        PreparedStatement prStat = null;

        String query = "UPDATE Collaborateur " +
                "SET  Nom = ?, Prenom = ? " +
                "WHERE ID = ?";

        try{
            prStat = connect.prepareStatement(query);
            prStat.setString(1, objectToUpdateInDB.getNom());
            prStat.setString(2, objectToUpdateInDB.getPrenom());
            prStat.setInt(3, objectToUpdateInDB.getID());

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
     * @param objectToDeleteFormDB l'objet Collaborateur
     * @return true si la ligne est bien supprimé, false sinon
     * @throws SQLException
     */
    public boolean delete(Collaborateur objectToDeleteFormDB) throws SQLException {
        boolean isDeleted = false;
        PreparedStatement prStat = null;

        String query = "DELETE FROM Collaborateur " +
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
     * cherche un Collaborateur avec son nom et son prénom
     * @param nom le nom du collaborateur
     * @param prenom le prenom du collaborateur
     * @return un Collaborateur correspondant aux nom et prénom donné
     * @throws SQLException
     */
    public Collaborateur findByNom(String nom, String prenom) throws SQLException {
        PreparedStatement prStat = null;

        ResultSet rs = null;
        Collaborateur collaborateurFind = null;

        String query = "SELECT ID, Nom, Prenom " +
                "FROM Collaborateur " +
                "WHERE Nom = ? AND Prenom = ?";

        try {
            prStat = connect.prepareStatement(query);
            prStat.setString(1, nom);
            prStat.setString(2, prenom);
            rs = prStat.executeQuery();
            if(rs.next()) {
                collaborateurFind = new Collaborateur(
                        rs.getInt("ID"),
                        rs.getString("Nom"),
                        rs.getString("Prenom")
                );
            }
        } finally {
            closeStatementAndResultSet(prStat, rs);
        }
        return collaborateurFind;
    }
}
