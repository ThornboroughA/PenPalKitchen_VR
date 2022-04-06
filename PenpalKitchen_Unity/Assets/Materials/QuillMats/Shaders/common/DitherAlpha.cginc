#ifndef DITHER_ALPHA_H
#define DITHER_ALPHA_H
#include "UnityCG.cginc"

inline void dither_mask(float4 screen_pos, float alpha) {
    float2 pos = (screen_pos.xy / screen_pos.w) * _ScreenParams.xy;

    float dither_matrix[16] = {
        1.0 / 17.0,  9.0 / 17.0,  3.0 / 17.0, 11.0 / 17.0,
        13.0 / 17.0,  5.0 / 17.0, 15.0 / 17.0,  7.0 / 17.0,
        4.0 / 17.0, 12.0 / 17.0,  2.0 / 17.0, 10.0 / 17.0,
        16.0 / 17.0,  8.0 / 17.0, 14.0 / 17.0,  6.0 / 17.0
    };

    int id = (int(fmod(pos.x, 4)) * 4 + int(fmod(pos.y, 4)));
    clip(alpha - dither_matrix[id]);
}

#endif /* DITHER_ALPHA_H */
