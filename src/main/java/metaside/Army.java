package metaside;

import com.sun.org.apache.xpath.internal.operations.Mod;

import java.io.File;
import java.util.ArrayList;
import java.util.List;
import java.util.Random;

/**
 * Created by Petr on 15.10.2016.
 */

//Reflective class, to lazy to implement normal ones.
public class Army {
    public static final int UNITNUMBER_MAX = 200;
    public static final int UNITNUMBER_MIN = 150;

    private String armyName;

    private List<? super FirstStrikeable> firstArmy = new ArrayList<>();
    private List<? super MiddleEarthCitizen> secondArmy = new ArrayList<>();

    public Army(String name) {
        armyName = name;
    }

    public void generateFirstArmy(List<Class<? extends FirstStrikeable>> unitTypes) {
        this.<FirstStrikeable>generateArmy(firstArmy, unitTypes);
    }

    public void generateSecondArmy(List<Class<? extends MiddleEarthCitizen>> unitTypes) {
        this.<MiddleEarthCitizen>generateArmy(secondArmy, unitTypes);
    }

    private <T> void generateArmy(List<? super T> units, List<Class<? extends T>> unitTypes) {
        try {
            for(Class c : unitTypes) {
                if(SingleUnit.class.isAssignableFrom(c)) {
                    if(BattleRandom.getInstance().throwCoin()) {
                        units.add((T)c.newInstance());
                    }
                } else {
                    int wariors = BattleRandom.getInstance().intBetween(UNITNUMBER_MIN, UNITNUMBER_MAX);
                    for(int i = 0; i < wariors; ++i) {
                        units.add((T)c.newInstance());
                    }
                }
            }
        } catch (InstantiationException e) {
            e.printStackTrace();
        } catch (IllegalAccessException e) {
            e.printStackTrace();
        }
    }

    public boolean firstRoundAttack(Army oppo) { //Loop through alive units
        int k = -1, e = -1;

        while(getFirstArmyAlives() > 0 && oppo.getFirstArmyAlives() > 0){
            int kw = getNextFAAlive(k);
            if(kw == -1) {
                return false; //defender won
            }

            int ew = oppo.getNextFAAlive(e);
            if(ew == -1){
                return true; //attacker won
            }

            MiddleEarthCitizen warrior = (MiddleEarthCitizen)getFirstArmy().get(kw);
            MiddleEarthCitizen enemy = (MiddleEarthCitizen)oppo.getFirstArmy().get(ew);

            warrior.applyAttack(enemy);
            if(!enemy.isDead()) {
                enemy.applyAttack(warrior);
            }

            k = kw;
            e = ew;
        }

        return true; //will not even be called
    }

    public boolean secondRoundAttack(Army oppo) {
        int k = -1, e = -1;

        while(getSecondArmyAlives() > 0 && oppo.getSecondArmyAlives() > 0){
            int kw = getNextSAAlive(k);
            if(kw == -1) {
                return false; //defender won
            }

            int ew = oppo.getNextSAAlive(e);
            if(ew == -1){
                return true; //attacker won
            }

            MiddleEarthCitizen warrior = (MiddleEarthCitizen)getSecondArmy().get(kw);
            MiddleEarthCitizen enemy = (MiddleEarthCitizen)oppo.getSecondArmy().get(ew);

            warrior.applyAttack(enemy);
            if(!enemy.isDead()) {
                enemy.applyAttack(warrior);
            }

            k = kw;
            e = ew;
        }

        return true; //will not even be called
    }

    public boolean thirdRoundAttack(Army oppo) {
        int fk = -1, fe = -1;
        int sk = -1, se = -1;

        while(getTotalAlives() > 0 && oppo.getTotalAlives() > 0){
            MiddleEarthCitizen warrior;
            MiddleEarthCitizen enemy;

            int fkw = getNextFAAlive(fk);
            if(fkw == -1) {
                int skw = getNextSAAlive(sk);
                if(skw == -1) {
                    return false;
                } else {
                    sk = skw;
                    warrior = (MiddleEarthCitizen)getSecondArmy().get(sk);
                }
            } else {
                fk = fkw;
                warrior = (MiddleEarthCitizen)getFirstArmy().get(fk);
            }

            int few = oppo.getNextFAAlive(fe);
            if(few == -1){
                int sew =oppo.getNextSAAlive(se);
                if(sew == -1) {
                    return true;
                } else {
                    se = sew;
                    enemy = (MiddleEarthCitizen)oppo.getSecondArmy().get(se);
                }
            } else {
                fe = few;
                enemy = (MiddleEarthCitizen)oppo.getFirstArmy().get(fe);
            }

            if(enemy instanceof HasBeast) {
                enemy.applyAttack(warrior);
                if(!warrior.isDead()) {
                    warrior.applyAttack(enemy);
                }
            } else {
                warrior.applyAttack(enemy);
                if (!enemy.isDead()) {
                    enemy.applyAttack(warrior);
                }
            }
        }

        return true; //will not even be called
    }

    public int getNextFAAlive(int cur) {
        int total = firstArmy.size();
        int i = cur + 1;
        while(i < total) {
            MiddleEarthCitizen unit = (MiddleEarthCitizen)firstArmy.get(i);
            if(!unit.isDead())
                return i;

            ++i;
        }

        i = 0;

        while(i <= cur)
        {
            MiddleEarthCitizen unit = (MiddleEarthCitizen)firstArmy.get(i);
            if(!unit.isDead())
                return i;

            ++i;
        }

        return cur;
    }

    public int getNextSAAlive(int cur) {
        int total = secondArmy.size();
        int i = cur + 1;
        while(i < total) {
            MiddleEarthCitizen unit = (MiddleEarthCitizen)secondArmy.get(i);
            if(!unit.isDead())
                return i;

            ++i;
        }

        i = 0;

        while(i <= cur)
        {
            MiddleEarthCitizen unit = (MiddleEarthCitizen)secondArmy.get(i);
            if(!unit.isDead())
                return i;

            ++i;
        }

        return cur;
    }

    public int getFirstArmyAlives() {
        int j = 0;
        for(int i = 0; i < firstArmy.size(); i++){
            if(!((MiddleEarthCitizen)firstArmy.get(i)).isDead()){
                j++;
            }
        }
        return j;
    }

    public int getSecondArmyAlives() {
        int j = 0;
        for(int i = 0; i < secondArmy.size(); i++){
            if(!((MiddleEarthCitizen)secondArmy.get(i)).isDead()){
                j++;
            }
        }
        return j;
    }

    public int getTotalAlives() {
        return getFirstArmyAlives() + getSecondArmyAlives();
    }

    public String getArmyName() {
        return armyName;
    }

    public void setArmyName(String armyName) {
        this.armyName = armyName;
    }

    public List<? super FirstStrikeable> getFirstArmy() {
        return firstArmy;
    }

    public void setFirstArmy(List<? super FirstStrikeable> firstArmy) {
        this.firstArmy = firstArmy;
    }

    public List<? super MiddleEarthCitizen> getSecondArmy() {
        return secondArmy;
    }

    public void setSecondArmy(List<? super MiddleEarthCitizen> secondArmy) {
        this.secondArmy = secondArmy;
    }
}
