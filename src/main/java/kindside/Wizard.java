package kindside;

import metaside.*;

/**
 * Created by Petr on 15.10.2016.
 */
public class Wizard extends MiddleEarthCitizen implements HasBeast, FirstStrikeable, SingleUnit{
    private Horse horse;
    private boolean firstStriked = false;

    public Wizard() {
        super(20);
        horse = new Horse();
    }

    public void setBeast(Beast beast) {
        if(beast instanceof Horse)
        {
            this.horse = (Horse) beast;
        }
    }

    public Beast getBeast() {
        return this.horse;
    }

    public int getBeastPower() {
        return this.horse.getPower();
    }

    public void applyAttack(MiddleEarthCitizen enemy) {
        enemy.setPower(enemy.getPower() - (!firstStriked ? getBeastPower() : 0) - getPower());
        firstStriked = true;
    }
}
