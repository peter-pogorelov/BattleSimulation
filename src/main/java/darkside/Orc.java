package darkside;

import metaside.Beast;
import metaside.FirstStrikeable;
import metaside.HasBeast;
import metaside.MiddleEarthCitizen;

/**
 * Created by Petr on 15.10.2016.
 */
public class Orc extends AbstractOrc implements FirstStrikeable, HasBeast {
    private Wolf wolf;
    private boolean firstStriked = false;

    public Orc() {
        this.wolf = new Wolf();
    }

    public void setBeast(Beast beast) {
        if(beast instanceof Wolf) {
            this.wolf = (Wolf) beast;
        }
    }

    public Beast getBeast() {
        return this.wolf;
    }

    public int getBeastPower() {
        return this.wolf.getPower();
    }

    public void applyAttack(MiddleEarthCitizen enemy) {
        enemy.setPower(enemy.getPower() - (!firstStriked ? getBeastPower() : 0) - getPower());
        firstStriked = true;
    }
}
