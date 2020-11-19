package cards.pay.paycardsrecognizer.sdk.ui;

import cards.pay.paycardsrecognizer.sdk.Card;

/**
 * Created by Mario Gamal on 3/31/20
 * Copyright © 2020 Tap Payments. All rights reserved.
 */
public interface InlineViewCallback {
    void onScanCardCanceled();
    void onScanCardFailed(Exception e);
    void onScanCardFinished(Card card, byte[] cardImage);
}
