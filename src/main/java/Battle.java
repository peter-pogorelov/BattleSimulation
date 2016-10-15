import darkside.*;
import kindside.*;
import metaside.Army;
import metaside.BattleRandom;
import metaside.FirstStrikeable;
import metaside.MiddleEarthCitizen;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by Petr on 15.10.2016.
 */
public class Battle {
    private Army kindArmy;
    private Army evilArmy;

    public Battle() {
        kindArmy = new Army("Kind");
        evilArmy = new Army("Evil");
    }

    public void fillKindArmy() {
        List<Class<? extends FirstStrikeable>> kindFirstArmy = new ArrayList<>();
        List<Class<? extends MiddleEarthCitizen>> kindSecondArmy = new ArrayList<>();

        kindFirstArmy.add(Wizard.class);
        kindFirstArmy.add(Rohhirim.class);
        kindArmy.generateFirstArmy(kindFirstArmy);

        kindSecondArmy.add(Human.class);
        kindSecondArmy.add(Elf.class);
        kindSecondArmy.add(WoodenElf.class);
        kindArmy.generateSecondArmy(kindSecondArmy);
    }

    public void fillEvilArmy() {
        List<Class<? extends FirstStrikeable>> evilFirstArmy = new ArrayList<>();
        List<Class<? extends MiddleEarthCitizen>> evilSecondArmy = new ArrayList<>();

        evilFirstArmy.add(Orc.class);
        evilArmy.generateFirstArmy(evilFirstArmy);

        evilSecondArmy.add(UrukHai.class);
        evilSecondArmy.add(Troll.class);
        evilSecondArmy.add(Goblin.class);
        evilArmy.generateSecondArmy(evilSecondArmy);
    }

    public Army getKindArmy() {
        return kindArmy;
    }

    public void setKindArmy(Army kindArmy) {
        this.kindArmy = kindArmy;
    }

    public Army getEvilArmy() {
        return evilArmy;
    }

    public void setEvilArmy(Army evilArmy) {
        this.evilArmy = evilArmy;
    }

    public static void main(String args[]) throws IllegalAccessException, InstantiationException {
        Battle battle = new Battle();

        battle.fillEvilArmy();
        battle.fillKindArmy();

        Army attacker;
        Army defender;

        boolean rnd = BattleRandom.getInstance().throwCoin();

        if(rnd) {
            attacker = battle.getEvilArmy();
            defender = battle.getKindArmy();
        } else {
            attacker = battle.getKindArmy();
            defender = battle.getEvilArmy();
        }

        System.out.println("The battle is beginig!");
        System.out.println("Attacker is " + attacker.getArmyName());
        System.out.println("Defender is " + defender.getArmyName());
        System.out.println(attacker.getArmyName() + " has " + attacker.getTotalAlives() + " units.");
        System.out.println(defender.getArmyName() + " has " + defender.getTotalAlives() + " units.");


        attacker.firstRoundAttack(defender);
        attacker.secondRoundAttack(defender);

        if(attacker.getTotalAlives() == 0) {
            System.out.println(defender.getArmyName() + " has won epic battle!");
        } else if(defender.getTotalAlives() == 0) {
            System.out.println(attacker.getArmyName() + " has won epic battle!");
        } else {
            if(attacker.thirdRoundAttack(defender)){
                System.out.println(attacker.getArmyName() + " has won epic battle!");
            } else {
                System.out.println(defender.getArmyName() + " has won epic battle!");
            }
        }

        System.out.println(attacker.getArmyName() + " " + attacker.getTotalAlives());
        System.out.println(defender.getArmyName() + " " + defender.getTotalAlives());
    }
}
