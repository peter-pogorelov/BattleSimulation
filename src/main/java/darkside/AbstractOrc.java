package darkside;

import metaside.MiddleEarthCitizen;

/**
 * Created by Petr on 15.10.2016.
 */
public abstract class AbstractOrc extends MiddleEarthCitizen {
    public AbstractOrc() {
        super(8, 10);
    }

    protected AbstractOrc(int minPower, int maxPower){
        super(minPower, maxPower);
    }
}
