package cards.pay.paycardsrecognizer.sdk;

/**
 * Created by AhlaamK on 6/1/20.
 * <p>
 * Copyright (c) 2020    Tap Payments.
 * All rights reserved.
 **/
public class FrameManager {

    /**
     *  Frame Background
     */
    private int                     frameColour;


    /**
     * Frame color
     * @param frameColour is frame
     */
    public FrameManager setFrameColor(int frameColour) {
        this.frameColour = frameColour;
        return this;
    }
/////////////////////////////////////////////////////////////////////
    /**
     * @return frame color
     */
    public int getFrameColor() {
        return this.frameColour;
    }
    //////////////////////////////////////////  Single Instance ////////////////////////

    /**
     * Get Shared instance of ThemeManager
     *
     * @return ThemeManager
     */
    public static FrameManager getInstance() {
        return SingleInstanceAdmin.instance;
    }


    private static class SingleInstanceAdmin {
        /**
         * The Instance.
         */
        public final static FrameManager instance = new FrameManager();

    }

}
