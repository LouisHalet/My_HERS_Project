package be.hers.Serveur.POJO;

public enum StatusTimingTache {
    DANS_LE_DELAI("Dans le délai"),
    EN_RETARD("En retard"),
    INTERMEDIAIRE("Intermediaire");

    private final String label;

    StatusTimingTache(String label) {
        this.label = label;
    }
    public static StatusTimingTache fromLabel(String label) {
        StatusTimingTache statusTache = null;
        for (StatusTimingTache s : values()) {
            if (s.label.equalsIgnoreCase(label)) {
                statusTache = s;
            }
        }
        return statusTache;
    }
    public String getLabel() {
        return label;
    }
    @Override
    public String toString() {
        return label;
    }

}
