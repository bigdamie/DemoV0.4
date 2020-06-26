// This is an example convo

INCLUDE vars.ink

// This will be for Lord Turnip

~frenLevel = intro

->convo
==convo

//Salutations
{ frenLevel:
-intro: nice to metcha
-acquantaince: howdy partner.
-homie: hey buddy uwu.
-bigHomie: owo wats that in ur pants *glomp* uwu
-else: Rigbert
}

//Response
+ {frenLevel == bigHomie} [Suck his dick.] -> dickSuck
+ {frenLevel > intro and not gaveGift} [Give em a gift.] -> giveGift
+ [haha u too]

-> continueConvo    
    ==continueConvo
    
    {gaveGift} {frenLevel} Mr. Turnip eyes you, longingly.
    ++ {frenLevel == bigHomie} [Suck his dick.] -> dickSuck
    ++ [Leave]
    oki bye
    
->DONE

==dickSuck
Miss me with that gay shit. 
-> continueConvo

==giveGift
Oh wow, thanks pal.
~gaveGift = true
~frenLevel++
-> continueConvo