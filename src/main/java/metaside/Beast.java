package metaside;

import java.util.Random;

/**
 * Created by Petr on 15.10.2016.
 */
public abstract class Beast {
    private int power;

    public Beast(int minPower, int maxPower){
        Random rand = new Random(System.currentTimeMillis());
        this.power = BattleRandom.getInstance().intBetween(minPower, maxPower);
    }

    public int getPower() {
        return power;
    }
}
