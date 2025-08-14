import React from "react";
import { Table, TableHeader, TableColumn, TableBody, TableRow, TableCell, Checkbox } from "@heroui/react";

interface Skill {
    id: string;
    name: string;
    attribute: string;
    isProficient: boolean;
    modifier: number;
}

export default function SkillsSection(){
    const [skills, setSkills] = React.useState<Skill[]>([
        { id: "acrobatics", name: "Acrobatics", attribute: "DEX", isProficient: false, modifier: 2 },
        { id: "animal-handling", name: "Animal Handling", attribute: "WIS", isProficient: false, modifier: 1 },
        { id: "arcana", name: "Arcana", attribute: "INT", isProficient: false, modifier: 1 },
        { id: "athletics", name: "Athletics", attribute: "STR", isProficient: true, modifier: 5 },
        { id: "deception", name: "Deception", attribute: "CHA", isProficient: false, modifier: 0 },
        { id: "history", name: "History", attribute: "INT", isProficient: false, modifier: 1 },
        { id: "insight", name: "Insight", attribute: "WIS", isProficient: true, modifier: 4 },
        { id: "intimidation", name: "Intimidation", attribute: "CHA", isProficient: true, modifier: 3 },
        { id: "investigation", name: "Investigation", attribute: "INT", isProficient: false, modifier: 1 },
        { id: "medicine", name: "Medicine", attribute: "WIS", isProficient: false, modifier: 1 },
        { id: "nature", name: "Nature", attribute: "INT", isProficient: false, modifier: 1 },
        { id: "perception", name: "Perception", attribute: "WIS", isProficient: true, modifier: 4 },
        { id: "performance", name: "Performance", attribute: "CHA", isProficient: false, modifier: 0 },
        { id: "persuasion", name: "Persuasion", attribute: "CHA", isProficient: false, modifier: 0 },
        { id: "religion", name: "Religion", attribute: "INT", isProficient: false, modifier: 1 },
        { id: "sleight-of-hand", name: "Sleight of Hand", attribute: "DEX", isProficient: false, modifier: 2 },
        { id: "stealth", name: "Stealth", attribute: "DEX", isProficient: false, modifier: 2 },
        { id: "survival", name: "Survival", attribute: "WIS", isProficient: true, modifier: 4 },
    ]);

    const toggleProficiency = (id: string) => {
        setSkills(skills.map(skill => {
            if (skill.id === id) {
                const isProficient = !skill.isProficient;
                // Adjust modifier based on proficiency (simplified)
                const baseModifier = skill.modifier - (skill.isProficient ? 3 : 0);
                const newModifier = baseModifier + (isProficient ? 3 : 0);

                return {
                    ...skill,
                    isProficient,
                    modifier: newModifier
                };
            }
            return skill;
        }));
    };

    return (
        <div>
            <h2 className="text-xl font-semibold mb-4 text-slate-800 dark:text-slate-200">Skills</h2>
            <Table
                removeWrapper
                aria-label="Skills table"
                classNames={{
                    th: "bg-slate-100 dark:bg-slate-800 text-slate-600 dark:text-slate-300",
                    td: "py-2"
                }}
            >
                <TableHeader>
                    <TableColumn>SKILL</TableColumn>
                    <TableColumn>MOD</TableColumn>
                </TableHeader>
                <TableBody>
                    {skills.map((skill) => (
                        <TableRow key={skill.id}>
                            <TableCell>{skill.name}</TableCell>
                            <TableCell className="font-medium">
                                {skill.modifier >= 0 ? `+${skill.modifier}` : skill.modifier}
                            </TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </div>
    );
}