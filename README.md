# BoneHurtingBullets
Allow bullets to break player's bones based on chance.

You can define the chance to break legs per limb

You can define a min damage required (BreakChanceDamageMin) to have a chance to break legs
This damage will result in the lowest defined chance (BreakChanceMin)

You can also define a max damage (BreakChanceDamageMax), to have the max chance (BreakChanceMax) to break legs

Recommended for legs:
```
<BreakChanceMin>10</BreakChanceMin>
<BreakChanceMax>95</BreakChanceMax>
<BreakChanceDamageMin>10</BreakChanceDamageMin>
<BreakChanceDamageMax>50</BreakChanceDamageMax>
```
This allows a 95% chance when shot with highcall sniper and does avoid breaking legs with painball gun
