package kindside;

import metaside.Beast;
import metaside.FirstStrikeable;
import metaside.HasBeast;
import metaside.MiddleEarthCitizen;

/**
 * Created by Petr on 15.10.2016.
 */
public class Rohhirim extends Human implements HasBeast, FirstStrikeable {
    private Horse horse;
    private boolean firstStriked = false;

    public Rohhirim() {
        this.horse = new Horse();
    }

    public void applyAttach(MiddleEarthCitizen enemy) {
        enemy.setPower(enemy.getPower() - (!firstStriked ? getBeastPower() : 0) - getPower());
        firstStriked = true;
    }

    public void setBeast(Beast beast) {
        if(beast instanceof Horse) {
            this.horse = (Horse)beast;
        }
    }

    public Beast getBeast() {
        return horse;
    }

    public int getBeastPower() {
        return horse.getPower();
    }
}
