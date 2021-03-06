% @Author: Mihai Gurei <mihaig>
% @Date:   "Sunday, 31st December 2017, 16:57:32"
% @Email:  mihai.gurei@analphabeta.com
% @Project: Sapiens
% @Filename: m_DeadMenCalc.m
% @License: lgpl3

%% Function that outputs the number of dead men after an attack.
% The result depends on the values of attacke and defenser. There are:
% - Experience ratio.
% - Formation modifier.
% - Damage calculated from pure damage / armor reduction.
% - Numbers and HP modifier.

function deadmen = m_DeadMenCalc(pure_damage, armor, paramA, paramD, charge)
    % Attacker
    % pure_damage(1:3): cut, blunt, pierce
    % paramA: to be defined
    % Defenser
    % armor(1:3): cur, blunt, pierce
    % patamD: to be defined
    % charge: [bool] indicates if attacker is charging

    % Experience
    % TODO: give an actual attrubution from param values
    expA = 1;
    expD = 1;
    expRatio = m_Ratio(expA, expD);  % Calc ration expA/expB

    % Formation modifier
    formMod = m_formMod(paramA, paramD);  % Just equal 1, for now.

    % Actual damage calculation
    damDealt = m_Damage(pure_damage, armor);

    % Numbers and unit HP modifier
    % TODO: Does this actually make any sense? gs cha
    num = 100;
    hpD = 50;
    numRatio = m_Ratio(num, hpD);

    deadmen_fight = formMod * expRatio * damDealt * numRatio;

    % Calculation of charge damage
    if (charge)
        deadmen_charge + formMod * 1;  % TODO: Finish formula
    end

    deadmen = deadmen_fight + deadmen_charge;
    fprintf('Result:\n');
    fprintf('Total dead: %d\n', deadmen);
    fprintf('Dead in charge: %d', deadmen_charge);
    fprintf('Deadman in fight: %d', deadmen_charge);
    % Charge damahe
    charge = false;     % TODO: get charge value from somewhere
    if (charge)

    end


end
