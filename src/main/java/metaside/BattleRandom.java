package metaside;

import java.util.Random;

/**
 * Created by Petr on 15.10.2016.
 */
public class BattleRandom {
    private static BattleRandom inst;
    private Random rand;

    private BattleRandom() {
        rand = new Random(System.currentTimeMillis());
    }

    public static BattleRandom getInstance() {
        if(inst == null) {
            inst = new BattleRandom();
        }

        return inst;
    }

    public int intBetween(int a, int b){
        return rand.nextInt(b - a + 1) + a;
    }

    public boolean throwCoin(){
        return rand.nextBoolean();
    }
}
