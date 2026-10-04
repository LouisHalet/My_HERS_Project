package be.hers.Serveur.POJO;

public enum StatusTache {
    NON_ENTAME("Non entamé"),
    CLOTUREES("Cloturées"),
    EN_COURS("En cours"),
    EN_PAUSE("En pause"),
    BLOQUEE("Bloquée"),
    ANNULEE("Annulée");

    private final String label;

    StatusTache(String label) {
        this.label = label;
    }
    public String getLabel() {
        return label;
    }
    public static StatusTache fromLabel(String label) {
        StatusTache statusTache = null;
        for (StatusTache s : values()) {
            if (s.label.equalsIgnoreCase(label)) {
                statusTache = s;
            }
        }
        return statusTache;
    }
    @Override
    public String toString() {
        return label;
    }

}
