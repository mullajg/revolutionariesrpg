import React from "react";
import { Input, Avatar, Select, SelectItem } from "@heroui/react";

export default function CharacterHeader(){
  const [characterClass, setCharacterClass] = React.useState("fighter");
  const [level, setLevel] = React.useState("1");
  const [race, setRace] = React.useState("human");
  const [background, setBackground] = React.useState("soldier");
  const [alignment, setAlignment] = React.useState("neutral");
  const [experience, setExperience] = React.useState("0");
  
  return (
    <div className="p-4 flex flex-row gap-4 bg-primary-100 dark:bg-primary-900/30 border-b border-primary-200 dark:border-primary-800">
      <div className="flex flex-col md:flex-row gap-4">
        <div className="flex-shrink-0">
        <Avatar
            src=""
            className="w-24 h-24 text-large"
            isBordered
            color="primary"
          />
        </div>
        <h1 className="text-xl font-semibold mb-4 text-slate-800 dark:text-slate-200">Avanti</h1>
        <h3>Herigate | Class</h3>
      </div>
    </div>
  );
}