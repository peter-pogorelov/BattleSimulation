package metaside;

import java.util.Random;

/**
 * Created by Petr on 15.10.2016.
 */
public abstract class MiddleEarthCitizen {
    private int power;
    private boolean dead;

    public MiddleEarthCitizen(int minPower, int maxPower){
        Random rand = new Random(System.currentTimeMillis());
        this.power = BattleRandom.getInstance().intBetween(minPower, maxPower);
        this.dead = false;
    }

    public MiddleEarthCitizen(int power) {
        this.power = power;
    }

    public void applyAttack(MiddleEarthCitizen enemy){
        enemy.setPower(enemy.getPower() - getPower());
    }

    public int getPower() {
        return power;
    }

    public void setPower(int power) {
        this.power = power;
        this.setDead(this.power <= 0);
    }

    public String toString() {
        return this.getClass().getName();
    }

    public boolean isDead() {
        return dead;
    }

    public void setDead(boolean dead) {
        this.dead = dead;
    }
}
