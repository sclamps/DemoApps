package cards.pay.paycardsrecognizer.sdk.ui;

import android.content.Context;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.support.v4.view.ViewCompat;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import cards.pay.paycardsrecognizer.sdk.Card;
import cards.pay.paycardsrecognizer.sdk.R;
import cards.pay.paycardsrecognizer.sdk.camera.RecognitionAvailabilityChecker;
import cards.pay.paycardsrecognizer.sdk.camera.RecognitionCoreUtils;
import cards.pay.paycardsrecognizer.sdk.camera.RecognitionUnavailableException;

public class InlineViewFragment extends Fragment implements ScanCardFragment.InteractionListener,
        InitLibraryFragment.InteractionListener{

    private InlineViewCallback callback;
    private static final String TAG = "ScanInlineView";

    public InlineViewFragment() {}

    @Override
    public void onAttach(Context context) {
        super.onAttach(context);
        callback = (InlineViewCallback) context;
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        return inflater.inflate(R.layout.fragment_inline_view, container, false);
    }

    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        if (savedInstanceState == null) {
            RecognitionAvailabilityChecker.Result checkResult = RecognitionAvailabilityChecker.doCheck(getActivity());
            if (checkResult.isFailed()
                    && !checkResult.isFailedOnCameraPermission()) {
                onScanCardFailed(new RecognitionUnavailableException(checkResult.getMessage()));
            } else {
                if (RecognitionCoreUtils.isRecognitionCoreDeployRequired(getActivity())
                        || checkResult.isFailedOnCameraPermission()) {
                    showInitLibrary();
                } else {
                    showScanCard();
                }
            }
        }
    }

    private void showScanCard() {
        Fragment fragment = new ScanCardFragment();
        getChildFragmentManager().beginTransaction()
                .replace(R.id.inline_content, fragment, ScanCardFragment.TAG)
                .setCustomAnimations(0, 0)
                .commitNow();

        ViewCompat.requestApplyInsets(getView().findViewById(R.id.inline_content));
    }

    private void showInitLibrary() {
        Fragment fragment = new InitLibraryFragment();
        getChildFragmentManager().beginTransaction()
                .replace(R.id.inline_content, fragment, InitLibraryFragment.TAG)
                .setCustomAnimations(0, 0)
                .commitNow();
    }
    @Override
    public void onInitLibraryFailed(Throwable e) {
        Log.e(TAG, "Init library failed", new RuntimeException("onInitLibraryFailed()", e));
    }

    @Override
    public void onInitLibraryComplete() {
        showScanCard();
    }

    @Override
    public void onScanCardCanceled(int cancelReason) {
        callback.onScanCardCanceled();
    }

    @Override
    public void onScanCardFailed(Exception e) {
        callback.onScanCardFailed(e);
    }

    @Override
    public void onScanCardFinished(Card card, byte[] cardImage) {
        callback.onScanCardFinished(card, cardImage);
    }
}
